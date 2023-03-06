
document.addEventListener("deviceready", function() {
	
    var mapCanvas;
    var markers = [];
    var paraDragEnd;
    var objLab;
    var geocoder;
    var markersVisivel = [];
    
      $('.marquee').marquee({
        //speed in milliseconds of the marquee
        duration: 5000,
        //gap in pixels between the tickers
        gap: 50,
        //time in milliseconds before the marquee will start animating
        delayBeforeStart: 0,
        //'left' or 'right'
        direction: 'left',
        //true or false - should the marquee be duplicated to show an effect of continues flow
        duplicated: true
      });  
      
      $('.close').click(function(e){
        $('.overlay').fadeToggle();
        $('#overlay-linhabus').fadeToggle();		
      });
      
      $('.overlay').click(function(e){
        $('#overlay-linhabus').fadeOut();
        $(this).fadeOut();	
      });
        
    $('.overlayConsulta').click(function(e){
        $('#overlay-linhabusConsulta').fadeOut();
        $(this).fadeOut();	
      });	
      
      $('#ponto').click(function(e){
        $('.overlay').fadeToggle();
        $('#overlay-linhabus').fadeToggle();
      });
        
        $('#btnConsulta').click(function(e){
            
        $('#overlay-linhabusConsulta').fadeOut();
        $('.overlayConsulta').fadeOut();
            
        var campoCpf = $('#cpfConsulta').val();    
        $.getJSON('http://busonline.com.br/api/exame/'+campoCpf, function(obj) {
            
            var nome = obj.nome;
            var cnh = obj.cnh;
            var cpf = obj.cpf;
            var data = obj.data;
            var lab = obj.laboratorio;
            var resultado = obj.resultado;
            var foto = obj.urlFoto;
            
            $("#avatarUsuario").attr("src", foto);
            $('#nomeUsuario').html(nome);
            $('#cnhResultado').html("CNH: "+cnh);
            $('#labResultado').html(lab);
            $('#diaResultado').html(data);
                    
            if(resultado == true){
                
                $("#iconeResultado").attr("src", "img/aprovado.fw.png");
                $('#textoResultado').html("Aprovado");
                
            }else{
                
                $("#iconeResultado").attr("src", "img/reprovado.fw.png");
                $('#textoResultado').html("Reprovado");
                
            }
                            
            $('.overlayResultado').fadeIn();
                
        })	
        
      });
        
        $('.overlayResultado').click(function(e){    
            $(this).fadeOut();	
          });
            
    function initialize() {
            
            var mapOptions = {
                disableDefaultUI: true,
                mapTypeId: 'roadmap',
                zoom: 15,
                clickableIcons: false,
                styles: [{ featureType: "poi", elementType: "labels", stylers: [{ visibility: "off" }]}],
             };
             
                    
            montaMapa();			
    
        function montaMapa(){
    
        mapCanvas = new google.maps.Map(document.getElementById("mapa"), mapOptions);
        geocoder = new google.maps.Geocoder;    
                    
            var onSuccess = function(position) {       	
                
                mapCanvas.setCenter({lat:position.coords.latitude, lng:position.coords.longitude});
                var latlng = {lat: position.coords.latitude, lng: position.coords.longitude};
                geocodeLatLng(geocoder, mapCanvas);
            };
        
        function onError(error) {
            alert('code: '    + error.code    + '\n' +
                  'message: ' + error.message + '\n');
        }
    
        navigator.geolocation.getCurrentPosition(onSuccess, onError);			
          
        }		
        
    }
          
    initialize();	
        
    ///////////////////////////////////////////////////////////////////////////
    
    $('#icon2').click(function(){
                
            var onSuccess = function(position) {       	
                
                mapCanvas.setCenter({lat:position.coords.latitude, lng:position.coords.longitude});
                var latlng = {lat: position.coords.latitude, lng: position.coords.longitude};
                geocodeLatLng(geocoder, mapCanvas);
            };
        
        function onError(error) {
            alert('code: '    + error.code    + '\n' +
                  'message: ' + error.message + '\n');
        }
    
        navigator.geolocation.getCurrentPosition(onSuccess, onError);
        
        mapCanvas.setZoom(15);
                
    })	  
    
        $('#icon3').click(function(){	
        
            $('.overlayConsulta').fadeToggle();
            $('#overlay-linhabusConsulta').fadeToggle();    
            document.getElementById('cpfConsulta').value = '';
            $("#cpfConsulta").focus();
            
        })
        
        
        mapCanvas.addListener('dragstart', function() {	
                
        console.log('inicio drag');
        deleteMarkers();
                            
        }); // fecha dragstart	
        
        function setMapOnAll() {
          for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
          }
        }
    
        function deleteMarkers() {
          setMapOnAll();
          markers = [];
        }
                                    
        mapCanvas.addListener('dragend', function() {
                    
            clearTimeout(paraDragEnd);
            paraDragEnd = setTimeout(function(){					
                                        
            geocodeLatLng(geocoder, mapCanvas);												
                            
            mapCanvas.addListener('dragstart', function() {	
                                            
                deleteMarkers();		
                            
            }); // fecha dragstart				
                    
            }, 1000); // fecha settimeout				
                    
        }); // fecha dragend
        
        mapCanvas.addListener('drag', function() {
                    
            clearTimeout(paraDragEnd);		
                    
        });
                    
        function geocodeLatLng(geocoder, mapCanvas) {
          var input = mapCanvas.getCenter().toUrlValue();
          var latlngStr = input.split(',', 2);
          var latlng = {lat: parseFloat(latlngStr[0]), lng: parseFloat(latlngStr[1])};
          geocoder.geocode({'location': latlng}, function(results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
              if (results[1]) {
                  
                markersVisivel = [];
                marker = '';
                  
                $.getJSON('http://busonline.com.br/api/labendereco', function(obj) {
                            
                objLab = obj;
                console.log('quant reg: '+objLab.length);
                console.log(markers);
                
                for (i = 0; i < objLab.length; i++) { 
                    
                var id = objLab[i].id;
                var latLaboratorio = objLab[i].latitude;
                var longLaboratorio = objLab[i].longitude;			
                                
                    console.log(objLab[i].latitude+'//'+objLab[i].longitude);			
                    
                    var lat1 = parseFloat(latlngStr[0]);
                    var lon1 = parseFloat(latlngStr[1]);
                    
                    console.log(parseFloat(latlngStr[0])+'//'+parseFloat(latlngStr[1]));
                                    
                    var lat2 = parseFloat(objLab[i].latitude);
                    var lon2 = parseFloat(objLab[i].longitude);					
                
                    var R = 6371; // Radius of the earth in km
                    var dLat = (lat2 - lat1) * Math.PI / 180;			 
                    var dLon = (lon2 - lon1) * Math.PI / 180;			  
                    p1 = (lat1 * Math.PI / 180);			  
                    p2 = (lat2 * Math.PI / 180);			  
                    var a = 
                        0.5 - Math.cos(dLat)/2 + 
                        Math.cos(p1) * Math.cos(p2) * 
                        (1 - Math.cos(dLon))/2;					
                                 
                      var distanciaAproximada = R * 2 * Math.asin(Math.sqrt(a));			  		  			  
                      var distanciaAproximadaFormat = distanciaAproximada*1000;			  			  
                      var distFinal = Math.round(distanciaAproximadaFormat);								
                  
                      if(distFinal <= 5000){ // tá no raio
                    
                        var latlngLab = {lat: parseFloat(latLaboratorio), lng: parseFloat(longLaboratorio)};			  
    
                        var marker = new google.maps.Marker({
                          position: latlngLab,
                          map: mapCanvas,					  
                          id: i+1,
                          numMarcador: i,
                          icon: {						
                            url: "img/pin-ponto01.fw.png",
                            scaledSize: new google.maps.Size(40,45),						
                            }
                        });
                      
                          markersVisivel.push(i);					
                      
                      }else{
                      
                        var latlngLab = {lat: parseFloat(latLaboratorio), lng: parseFloat(longLaboratorio)};			  	
    
                        var marker = new google.maps.Marker({
                          position: latlngLab,
                          map: mapCanvas,
                          visible: false,
                        });				  
                  }				
                    
                    google.maps.event.addListener(marker, 'click', idMarcador);
                    markers.push(marker);			  							  
                }			  
                  
              });		   
                                  
            }		
        }
              
        });			
    }
                    
        function idMarcador(e) {		
            
            for (var j = 0; j < markersVisivel.length; j++) {			
                
                markers[markersVisivel[j]].setIcon({
                url: "img/pin-ponto01.fw.png",
                scaledSize: new google.maps.Size(40,45)
           })
                        
        }		
            
        $('.overlay').fadeToggle();
        $('#overlay-linhabus').fadeToggle();
                
            var numM = this.numMarcador;
            
            markers[numM].setIcon({
                url: "img/pin-pontoactive.png",
                scaledSize: new google.maps.Size(50,56)
            })
            
            var endereco = objLab[numM].endereco;
            var horario = objLab[numM].horario;
            var nomeLab = objLab[numM].nome;		
            
            var montaPonto = '<strong><span id="endCaixa">'+endereco+'</span></br><span id="endCaixa01"></span></strong><br>Horário de Funcionamento:</br><span id="horaCaixa" style="font-size: 0.5em;">'+horario+'</span></br><span id="horaCaixa01"></span>';
        
        $("#nomeLabCaixa").html(nomeLab);
        $("#dadosLab").html(montaPonto);	
                    
        }		
                
        function criaPontosDragEnd(){
        
        var posicao = ['-22.901623, -43.105316','-22.903903, -43.103811','-22.906277, -43.106337','-22.907852, -43.109705','-22.902724, -43.115057'];
    
            for(i=0; i<posicao.length; i++){
    
            var latlngStr = posicao[i].split(',', 3);
            var latlng = {lat: parseFloat(latlngStr[0]), lng: parseFloat(latlngStr[1])};
            var latFormat = parseFloat(latlngStr[0]);
            var longFormat = parseFloat(latlngStr[1]);
            var posFormat = latFormat+','+longFormat;
    
            var marker = new google.maps.Marker({
                    position: latlng,
                    map: mapCanvas,				
                    id: i+1,
                    numMarcador: i,
                    icon: {						
                        url: "img/pin-ponto01.fw.png",
                        scaledSize: new google.maps.Size(40,45),											
                        }				
                    });
    
                google.maps.event.addListener(marker, 'click', idMarcadorDrag);
                
                markers.push(marker);
        }
        
    } ///// fecha pontos dragend	
        
        function idMarcadorDrag(e) {		
            
             for (var j = 0; j < markers.length; j++) {			
                
                markers[j].setIcon({
                url: "img/pin-ponto01.fw.png",
                scaledSize: new google.maps.Size(40,45)
            })
                        
        }
            
        $('.overlay').fadeToggle();
        $('#overlay-linhabus').fadeToggle();
                
            var numM = this.numMarcador;
            
            markers[numM].setIcon({
                url: "img/pin-pontoactive.png",
                scaledSize: new google.maps.Size(50,56)
            })
            
            var endereco = objLab[numM].endereco;
            var horario = objLab[numM].horario;
            var nomeLab = objLab[numM].nome;				
        
            var montaPonto = '<strong><span id="endCaixa">'+endereco+'</span></br><span id="endCaixa01"></span></strong><br>Horário de Funcionamento:</br><span id="horaCaixa" style="font-size: 0.5em;">'+horario+'</span></br><span id="horaCaixa01"></span>';
        
        $("#nomeLabCaixa").html(nomeLab);
        $("#dadosLab").html(montaPonto);		
                    
        }
        
    }); // fecha deviceready
          
    