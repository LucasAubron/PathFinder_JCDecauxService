var map = new ol.Map({
    target: 'map', // <-- This is the id of the div in which the map will be built.
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],

    view: new ol.View({
        center: ol.proj.fromLonLat([7.0985774, 43.6365619]), // <-- Those are the GPS coordinates to center the map to.
        zoom: 10 // You can adjust the default zoom.
    })

});

function getPaths(){
    var start = document.getElementById("start").value;
    var end = document.getElementById("end").value;
    //start="80 Boulevard Françoise Duparc, 13004 Marseille, France"
    //end="2 Grand Rue, 13002 Marseille, France"
    var targetUrl = "http://localhost:8733/Design_Time_Addresses/Routing/Service1/REST/Paths?start="+ start +"&end=" + end;
    //var targetUrl = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=e3e335df65e7d602cef98a312a3a710335a2d268";
    var requestType = "GET";
    console.log(targetUrl)

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    caller.setRequestHeader ("Accept", "application/json");
    caller.onload=displayPaths;
    caller.send();
}

function displayPaths(){
    var response = JSON.parse(this.responseText);
    response = JSON.parse(response)

    console.log(response)
    console.log(response[0].features[0].geometry.coordinates)

    response.forEach((path) => {
        displayPath(path);
    });
}

function displayPath(path){
    var coordinates = path.features[0].geometry.coordinates;
    for (let i=0; i<coordinates.length-1; i++){
        var coords = [coordinates[i], coordinates[i+1]];
        var lineString = new ol.geom.LineString(coords);

        // Transform to EPSG:3857
        lineString.transform('EPSG:4326', 'EPSG:3857');

        // Create the feature
        var feature = new ol.Feature({
            geometry: lineString,
            name: 'Line'
        });

        // Configure the style of the line
        var lineStyle = new ol.style.Style({
            stroke: new ol.style.Stroke({
                color: '#1E90FF',
                width: 10
            })
        });

        var source = new ol.source.Vector({
            features: [feature]
        });

        var vector = new ol.layer.Vector({
            source: source,
            style: [lineStyle]
        });

        map.addLayer(vector);
    }
}