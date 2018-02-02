/* function es_vacio()
  {
   if (document.getElementById('Name').value != "")
      {	  
        document.getElementById('Name').disabled=true;
        document.getElementById('Description').disabled=true;
       }
   }	 */	    


function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else { 
        document.getElementById("Longitude").Value = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var x = position.coords.latitude;
    var y = position.coords.longitude;
    document.getElementById("Latitude").value = x; 
    document.getElementById("Longitude").value = y;
}


  function geocodeAddress(geocoder, resultsMap) {
    var address = document.getElementById('address').value;
    geocoder.geocode({'address': address}, function(results, status) {
      if (status === 'OK') {
        resultsMap.setCenter(results[0].geometry.location);
        var marker = new google.maps.Marker({
          map: resultsMap,
          position: results[0].geometry.location
          
        });
        document.getElementById("Latitud").value = results[0].geometry.location;
        document.getElementById("Longitud").value = results[0].geometry.location;
      } 
      else {
        alert('Geocode was not successful for the following reason: ' + status);
      }
    });
  }