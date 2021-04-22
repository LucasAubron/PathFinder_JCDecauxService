var map = new ol.Map({
    target: 'map', // <-- This is the id of the div in which the map will be built.
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM(),
            name: "map"
        })
    ],

    view: new ol.View({
        center: ol.proj.fromLonLat([7.0985774, 43.6365619]), // <-- Those are the GPS coordinates to center the map to.
        zoom: 10 // You can adjust the default zoom.
    })

});

var lastLayers = [];

function colourNameToHex(colour)
{
    var colours = {"aliceblue":"#f0f8ff","antiquewhite":"#faebd7","aqua":"#00ffff","aquamarine":"#7fffd4","azure":"#f0ffff",
    "beige":"#f5f5dc","bisque":"#ffe4c4","black":"#000000","blanchedalmond":"#ffebcd","blue":"#0000ff","blueviolet":"#8a2be2","brown":"#a52a2a","burlywood":"#deb887",
    "cadetblue":"#5f9ea0","chartreuse":"#7fff00","chocolate":"#d2691e","coral":"#ff7f50","cornflowerblue":"#6495ed","cornsilk":"#fff8dc","crimson":"#dc143c","cyan":"#00ffff",
    "darkblue":"#00008b","darkcyan":"#008b8b","darkgoldenrod":"#b8860b","darkgray":"#a9a9a9","darkgreen":"#006400","darkkhaki":"#bdb76b","darkmagenta":"#8b008b","darkolivegreen":"#556b2f",
    "darkorange":"#ff8c00","darkorchid":"#9932cc","darkred":"#8b0000","darksalmon":"#e9967a","darkseagreen":"#8fbc8f","darkslateblue":"#483d8b","darkslategray":"#2f4f4f","darkturquoise":"#00ced1",
    "darkviolet":"#9400d3","deeppink":"#ff1493","deepskyblue":"#00bfff","dimgray":"#696969","dodgerblue":"#1e90ff",
    "firebrick":"#b22222","floralwhite":"#fffaf0","forestgreen":"#228b22","fuchsia":"#ff00ff",
    "gainsboro":"#dcdcdc","ghostwhite":"#f8f8ff","gold":"#ffd700","goldenrod":"#daa520","gray":"#808080","green":"#008000","greenyellow":"#adff2f",
    "honeydew":"#f0fff0","hotpink":"#ff69b4",
    "indianred ":"#cd5c5c","indigo":"#4b0082","ivory":"#fffff0","khaki":"#f0e68c",
    "lavender":"#e6e6fa","lavenderblush":"#fff0f5","lawngreen":"#7cfc00","lemonchiffon":"#fffacd","lightblue":"#add8e6","lightcoral":"#f08080","lightcyan":"#e0ffff","lightgoldenrodyellow":"#fafad2",
    "lightgrey":"#d3d3d3","lightgreen":"#90ee90","lightpink":"#ffb6c1","lightsalmon":"#ffa07a","lightseagreen":"#20b2aa","lightskyblue":"#87cefa","lightslategray":"#778899","lightsteelblue":"#b0c4de",
    "lightyellow":"#ffffe0","lime":"#00ff00","limegreen":"#32cd32","linen":"#faf0e6",
    "magenta":"#ff00ff","maroon":"#800000","mediumaquamarine":"#66cdaa","mediumblue":"#0000cd","mediumorchid":"#ba55d3","mediumpurple":"#9370d8","mediumseagreen":"#3cb371","mediumslateblue":"#7b68ee",
    "mediumspringgreen":"#00fa9a","mediumturquoise":"#48d1cc","mediumvioletred":"#c71585","midnightblue":"#191970","mintcream":"#f5fffa","mistyrose":"#ffe4e1","moccasin":"#ffe4b5",
    "navajowhite":"#ffdead","navy":"#000080",
    "oldlace":"#fdf5e6","olive":"#808000","olivedrab":"#6b8e23","orange":"#ffa500","orangered":"#ff4500","orchid":"#da70d6",
    "palegoldenrod":"#eee8aa","palegreen":"#98fb98","paleturquoise":"#afeeee","palevioletred":"#d87093","papayawhip":"#ffefd5","peachpuff":"#ffdab9","peru":"#cd853f","pink":"#ffc0cb","plum":"#dda0dd","powderblue":"#b0e0e6","purple":"#800080",
    "rebeccapurple":"#663399","red":"#ff0000","rosybrown":"#bc8f8f","royalblue":"#4169e1",
    "saddlebrown":"#8b4513","salmon":"#fa8072","sandybrown":"#f4a460","seagreen":"#2e8b57","seashell":"#fff5ee","sienna":"#a0522d","silver":"#c0c0c0","skyblue":"#87ceeb","slateblue":"#6a5acd","slategray":"#708090","snow":"#fffafa","springgreen":"#00ff7f","steelblue":"#4682b4",
    "tan":"#d2b48c","teal":"#008080","thistle":"#d8bfd8","tomato":"#ff6347","turquoise":"#40e0d0",
    "violet":"#ee82ee",
    "wheat":"#f5deb3","white":"#ffffff","whitesmoke":"#f5f5f5",
    "yellow":"#ffff00","yellowgreen":"#9acd32"};

    if (typeof colours[colour.toLowerCase()] != 'undefined')
        return colours[colour.toLowerCase()];

    return false;
}

function getPaths(){
    erasePaths();
    var start = document.getElementById("start").value;
    var end = document.getElementById("end").value;
    //few examples to try
    //start="80 Boulevard Françoise Duparc, 13004 Marseille, France"
    //end="2 Grand Rue, 13002 Marseille, France"
    //8 rue de l'ouest, Luxembourg
    //4 rue Emile Bian, Luxembourg
    var targetUrl = "http://localhost:8733/Design_Time_Addresses/Routing/Service1/REST/Paths?start="+ start +"&end=" + end;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    caller.setRequestHeader ("Accept", "application/json");
    caller.onload=drawPaths;
    caller.send();
}

function drawPaths(){
    var response = JSON.parse(this.responseText);
    if (response.slice(0,3)==404){
        console.log("Couldn't find the requested address.")
        document.getElementById("errorOrSuccess").innerHTML="Couldn't find the requested address.";

    } else {
        response = JSON.parse(response)
        var start = response[0].features[0].geometry.coordinates.slice(-1)[0]
        var end = response.slice(-1)[0].features[0].geometry.coordinates.slice(-1)[0]
        document.getElementById("errorOrSuccess").innerHTML="Request successfull: from coordinates " + start + " to coordinates " + end;
        fitMap(start, end)
        var index = 0;
        colors=[colourNameToHex("darkviolet"), colourNameToHex("lawngreen")];
        response.forEach((path) => {
            drawPath(path, colors[index]);
            if (index==1){ index--; }
            else { index++; }
        });
    }
    console.log(map)
}

function drawPath(path, color){
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
                color: color,
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
        lastLayers.push(vector)
    }
}


function fitMap(coord1, coord2){
    var lonmin = Math.min(coord1[0], coord2[0])
    var lonmax = Math.max(coord1[0], coord2[0])
    var latmin = Math.min(coord1[1], coord2[1])
    var latmax = Math.max(coord1[1], coord2[1])
    var coordMin = ol.proj.fromLonLat([lonmin, latmin], 'EPSG:3857');
    var coordMax = ol.proj.fromLonLat([lonmax, latmax], 'EPSG:3857');
    var extent=[coordMin[0],coordMin[1],coordMax[0],coordMax[1]];
    map.getView().fit(extent, map.getSize());
    map.getView().animate({
      zoom: map.getView().getZoom() - 1,
      duration: 250
    })
}

function erasePaths(){
    map.getLayers().getArray()
      .filter(layer => layer.get('name') != 'map')
      .forEach(layer => map.removeLayer(layer));
}