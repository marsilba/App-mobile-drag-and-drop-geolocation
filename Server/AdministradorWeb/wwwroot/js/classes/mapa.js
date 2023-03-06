var geocoder;
var map;
var marker;
var contaMapa = 0;

function initialize() {
	var latlng = new google.maps.LatLng(-22.9009949, -43.1133374);
	var options = {
		zoom: 13,
		center: latlng,
		clickableIcons: false,
		disableDefaultUI: true,
		mapTypeId: google.maps.MapTypeId.ROADMAP,
		styles: [{ featureType: "poi", elementType: "labels", stylers: [{ visibility: "off" }]},
    {
        "featureType": "all",
        "elementType": "labels.text.fill",
        "stylers": [
            {
                "color": "#7c93a3"
            },
            {
                "lightness": "-10"
            }
        ]
    },
    {
        "featureType": "administrative.country",
        "elementType": "geometry",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "administrative.country",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#a0a4a5"
            }
        ]
    },
    {
        "featureType": "administrative.province",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#62838e"
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#dde3e3"
            }
        ]
    },
    {
        "featureType": "landscape.man_made",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#3f4a51"
            },
            {
                "weight": "0.30"
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "simplified"
            }
        ]
    },
    {
        "featureType": "poi.attraction",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "poi.business",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "poi.government",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "poi.park",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "poi.place_of_worship",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "poi.school",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "poi.sports_complex",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "all",
        "stylers": [
            {
                "saturation": "-100"
            },
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#bbcacf"
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "lightness": "0"
            },
            {
                "color": "#bbcacf"
            },
            {
                "weight": "0.50"
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "labels",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "labels.text",
        "stylers": [
            {
                "visibility": "on"
            }
        ]
    },
    {
        "featureType": "road.highway.controlled_access",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#ffffff"
            }
        ]
    },
    {
        "featureType": "road.highway.controlled_access",
        "elementType": "geometry.stroke",
        "stylers": [
            {
                "color": "#a9b4b8"
            }
        ]
    },
    {
        "featureType": "road.arterial",
        "elementType": "labels.icon",
        "stylers": [
            {
                "invert_lightness": true
            },
            {
                "saturation": "-7"
            },
            {
                "lightness": "3"
            },
            {
                "gamma": "1.80"
            },
            {
                "weight": "0.01"
            }
        ]
    },
    {
        "featureType": "transit",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "color": "#a3c7df"
            }
        ]
    }
],
	};
	
	map = new google.maps.Map(document.getElementById("mapa"), options);
	
	geocoder = new google.maps.Geocoder();
	
	marker = new google.maps.Marker({
		map: map,
		draggable: true,
		visible: false
	});
	
	marker.setPosition(latlng);
}

$(document).ready(function () {
	
	initialize();
			
	$("#txtEndereco").focus(function(){
		this.select();
	});
		
	
	google.maps.event.addListener(map, 'click', function(event) {
		
		marker.setMap(null);		
		placeMarker(event.latLng);
				
		function placeMarker(location) {
			    marker = new google.maps.Marker({
				position: location, 
				draggable:true,
				map: map
			});
			
			map.setCenter(location);
			map.setZoom(16);
								
			geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (results[0]) {  
					$('#txtEndereco').val(results[0].formatted_address);
					$('#txtLatitude').val(marker.getPosition().lat());
					$('#txtLongitude').val(marker.getPosition().lng());
				}
			}
		});
			
			
		google.maps.event.addListener(marker, 'dragend', function () {
		geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (results[0]) {  
					$('#txtEndereco').val(results[0].formatted_address);
					$('#txtLatitude').val(marker.getPosition().lat());
					$('#txtLongitude').val(marker.getPosition().lng());
					
					zoomAtual = map.getZoom();
									
					if(zoomAtual < 16){
						map.setCenter(marker.getPosition());
						map.setZoom(16);
									
					}else{
						map.setCenter(marker.getPosition());
					}
										
				}
			}
		});
	});
			
		$("#txtEndereco").blur(); 
			
	}
});
	
	
	
	//.ui-menu-item
	//.ui-helper-hidden-accessible
	
	$("#txtEndereco").autocomplete({
		//appendTo: "#txtEndereco",		
		source: function (request, response) {
			//geocoder.geocode({ 'address': request.term , componentRestrictions: {country: 'BR', administrativeArea: 'Rio de Janeiro'}}, function (results, status) {
			geocoder.geocode({ 'address': request.term , componentRestrictions: {country: 'BR', administrativeArea: 'RJ', locality:'Niterói'}}, function (results, status) {
			//geocoder.geocode({ 'address': request.term + ', Brasil', 'region': 'BR' }, function (results, status) {
				response($.map(results, function (item) {
					return {
						label: item.formatted_address,
						value: item.formatted_address,
						latitude: item.geometry.location.lat(),
          				longitude: item.geometry.location.lng()
						
					}
				}));
			})
		},
			
		select: function (event, ui) {
			
			var latitude_destino_ui = ui.item.latitude;
    		var longitude_destino_ui = ui.item.longitude;
			geocodePosicao(geocoder,map,latitude_destino_ui,longitude_destino_ui);
			$("#txtEndereco").blur();
						
		}				
	});
	
	//$("#txtEndereco").autocomplete({focus:function(e,ui) {
    	//alert(ui.label);
		//$(this).val(ui);		
	//}});
	
function geocodePosicao(geocoder,map,latAt,longAt) {  
  var input = latAt+','+longAt;
  var latlngStr = input.split(',', 2);
  var latlng = {lat: parseFloat(latlngStr[0]), lng: parseFloat(latlngStr[1])};
  geocoder.geocode({'location': latlng}, function(results, status) {
    if (status === google.maps.GeocoderStatus.OK) {
		if (results[0]) {
			$('#txtEndereco').val(results[0].formatted_address);
			$('#txtLatitude').val(results[0].geometry.location.lat());
			$('#txtLongitude').val(results[0].geometry.location.lng());
			var location = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
			marker.setVisible(true);
			marker.setPosition(location);
			map.setCenter(location);
			map.setZoom(16);
		}
	}
  	
  });
	
	
	google.maps.event.addListener(marker, 'dragend', function () {
		geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (results[0]) {  
					$('#txtEndereco').val(results[0].formatted_address);
					$('#txtLatitude').val(marker.getPosition().lat());
					$('#txtLongitude').val(marker.getPosition().lng());
					
					zoomAtual = map.getZoom();
									
					if(zoomAtual < 16){
						map.setCenter(marker.getPosition());
						map.setZoom(16);
									
					}else{
						map.setCenter(marker.getPosition());
					}
										
				}
			}
		});
	});
	
	$("#txtEndereco").blur();		
}
		  
			

});