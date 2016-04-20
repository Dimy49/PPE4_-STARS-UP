package com.example.ppe.starsup;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;




public class Planning_Activity extends Activity {

    // Progress Dialog
    private ProgressDialog pDialog;

    // Creating JSON Parser object
    JSONParser jParser = new JSONParser();

    ArrayList<HashMap<String, String>> listVisite;

    // url to get all products list
    private static String get_visites = "http://192.168.215.10/ppe4-stars-up/get_visites.php";

    // products JSONArray
    JSONArray visites = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_planning);

        //Liste des visites
        listVisite = new ArrayList<HashMap<String, String>>();


        //chargement des visites
        new ChargementVisite().execute();

        ListView list_visite = (ListView) findViewById(R.id.list_visites);

        //OnClick ****************************************************************************************** à faire
        list_visite.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> a, View v, int position, long id) {
                Intent i = new Intent(getApplicationContext(), Hebergement_Activity.class);
                startActivity(i);
            }
        });
    }



    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_planning, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }



    class ChargementVisite extends AsyncTask<String, String, String> {

        // Before starting background thread Show Progress Dialog
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            pDialog = new ProgressDialog(Planning_Activity.this);
            pDialog.setMessage("Chargement des visites. Attendez ...");
            pDialog.setIndeterminate(false);
            pDialog.setCancelable(false);
            pDialog.show();
        }


        // getting All visites from url
        protected String doInBackground(String... args) {
            String idInspecteur = "4";

            // Building Parameters
            List<NameValuePair> params = new ArrayList<NameValuePair>();
            params.add(new BasicNameValuePair("id", idInspecteur));

            // getting JSON string from URL
            JSONObject json = jParser.makeHttpRequest(get_visites, "GET", params);

            // Check your log cat for JSON reponse
            Log.d("Visites : ", json.toString());

            try {
                // Checking for SUCCESS TAG
                int success = json.getInt("success");

                if (success == 1) {
                    // visites trouvées
                    // Getting Array of Products
                    visites = json.getJSONArray("visites");

                    // looping through All Products
                    for (int i = 0; i < visites.length(); i++) {
                        JSONObject v = visites.getJSONObject(i);

                        // creating new HashMap
                        HashMap<String, String> map = new HashMap<String, String>();

                        // adding each child node to HashMap key => value
                        map.put("nom", v.getString("nom"));
                        map.put("adresse", v.getString("adresse") + " " + v.getString("ville"));
                        map.put("date", v.getString("horaire"));

                        // adding HashList to ArrayList
                        listVisite.add(map);
                    }
                } else {
                    // no products found
                    // Launch Add New product Activity
                    //Intent i = new Intent(getApplicationContext(), Planning_Activity.class);
                    // Closing all previous activities
                    // i.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    //startActivity(i);
                }
            } catch (JSONException e) {
                e.printStackTrace();
            }

            return null;
        }

        //After completing background task Dismiss the progress dialog
        protected void onPostExecute(String file_url) {
            // dismiss the dialog after getting all products
            pDialog.dismiss();
            // updating UI from Background Thread
            runOnUiThread(new Runnable() {
                public void run() {
                    //Updating parsed JSON data into ListView

                    //Insertion des items dans la vue list_planning
                    ListAdapter adapter = new SimpleAdapter(Planning_Activity.this, listVisite, R.layout.list_planning,
                            new String[] {"date", "nom", "adresse"}, new int[] {R.id.date, R.id.nom, R.id.adresse});

                    setListAdapter(adapter);
                }
            });
        }
    }
}
