/**
 * Created by Alex Cifuentes Sanchez
 * 11/03/2017.
 */
//tener en cuenta se modifico el archivo materialize.js para que aceptara valores en español
//desde las lineas 806
//Documentacion de graficos:
//https://docs.tibco.com/pub/spotfire_web_player/6.0.0-november-2013/es-ES/WebHelp/GUID-8B1036A5-6BE9-4A84-B532-8E15060CABA9.html

$(document).ready(function () {
  
    $("#contenedor").removeClass();
  
  getAllME();

});

function getAllME() {
    var token = $('#codigospan').text();

    $.ajax({
        type: "GET",
        url: baseURL + "api/Usuarios/GetAllDatos",
        data: { codigo: token },
        success: function (response) {
          //console.log(response);
            $.each(response, function (i, v) {
                var apellido;
                if (v.apellidos == null) {
                    apellido = "";
                } else {
                    apellido = v.apellidos;
                }
            $("#imguser").attr("src", v.imagen);
            $("#nombreuserspan").html(v.nombres + " " + apellido);
            $('#emailspan').html(v.correo);
            if (v.nombre_rol === "Administrador") {
              $("#tokenacces").append('<li id="li-administracion"><a href="/Administracion"><i class="material-icons">power</i>Administración</a></li>');
            }
            if ($("#divclaims")!=undefined) {
              $("#divclaims").append('<input type="hidden" name="idUsuario" id="idUsuario" value="'+v.usuario_id+'" />');  
            }

            ripscargadostodosanios(v.usuario_id);
    
          });
    
        }
      }).fail(function (jqXHR, textStatus, errorThrown) {
        //si retorna un error es por que el correo no existe imprimo en consola y recargo pagina de inicio de sesión    console.error(textStatus, errorThrown); 
        //console.error(textStatus, errorThrown); // Algo fallo
          window.location.href = baseURL + "Cuenta";
        //envio a la api errores para que almacene el error
      })
  //
  //ripscargadosyestados();
}

// pinto el primer cuadro mostrando la cantida de cargados
function ripscargadostodosanios(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarCantidadaniosCargadosaWebPreradicación',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {

            var Anio = [];
            var total = [];

            $.each(data, function (k, v) {
                if (v.Anio == (new Date()).getFullYear()) { //recorro el vector y valido que sea el anio actual
                    //envio a la primera tarjeta el valor retornado por la api
                    $('#total_rips_anio_actual').html('Tiene un total de ' + v.Cantidad + ' envíos cargados en el año ' + v.Anio + '.')
                }
                if (v.Cantidad == 0) {
                    v.Anio = "Sin definir"
                }
               
            });

         
        },
        error: function (data) {
            console.log(data);
        }
    });

    ripscantenviostodosanios(token);
}


// pinto el pie de total de envios cuadro realizados al sistema
function ripscantenviostodosanios(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarCantidadaniosCargadosaWebValidacion',
        method: 'GET',
        data: {iduser:token},
        beforeSend: function () {
        },
        success: function (data) {
            
            var Anio = [];
            var total = [];

            $.each(data, function (k, v) {
               
                if (v.Cantidad == 0) {
                    v.Anio = "Sin definir"
                }
                
                Anio.push(v.Anio);
                total.push(parseInt(v.Cantidad));
            });

            var chartdata = {
                labels: Anio,
                datasets: [
                    {
                        borderWidth: 1,
                        backgroundColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear
                        ],
                        borderColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear
                        ],
                        hoverBackgroundColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear

                        ],
                        hoverBorderColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear
                        ],
                        data: total
                    }
                ]
            };

            var ctx = $("#canvascantidadestadoripscargadostodosanios");
            //options
            var option = {

                responsive: true,
                title: {
                    display: true,
                    position: "top",
                    text: "Cantidad de envíos por años",
                    fontSize: 10,
                },
                legend: {
                    display: true,
                    position: "bottom",
                    labels: {
                        fontSize: 10
                    }
                }

            };


            var barGraph = new Chart(ctx, {
                type: 'pie',
                data: chartdata,
                options: option
            });
        },
        error: function (data) {
            console.log(data);
        }
    });

    ripsradicadoscincoanios(token);
}

/**
 * 
 */
// pinto el segundo pie mostrando la cantida de rips radicados ultimos 5 años
function ripsradicadoscincoanios(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarCantidadaniosRadicados',
        method: 'GET',
        data: {iduser:token},
        beforeSend: function () {
        },
        success: function (data) {
            
            var Anio = [];
            var total = [];

            $.each(data, function (k, v) {
                if (v.Anio == (new Date()).getFullYear()) { //recorro el vector y valido que sea el anio actual
                    //envio a la primera tarjeta el valor retornado por la api
                    $('#total_rips_anio_actual').html('Tiene un total de ' + v.Cantidad +' envíos cargados en el año '+v.Anio+'.')
                }
                if (v.Cantidad == 0) {
                    v.Anio = "Sin definir"
                }
                
                Anio.push(v.Anio);
                total.push(parseInt(v.Cantidad));
            });

            var chartdata = {
                labels: Anio,
                datasets: [
                    {
                        borderWidth: 1,
                        backgroundColor: [
						/*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear,							
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            
                        ],
                        borderColor: [
						/*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear,					
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            
                        ],
                        hoverBackgroundColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear

                        ],
                        hoverBorderColor: [
                            window.chartColors.orange,
                            window.chartColors.silver,
                            window.chartColors.yellow,
                            window.chartColors.green,
                            window.chartColors.turkesa,
                            window.chartColors.blue,
                            window.chartColors.purple,
                            window.chartColors.grey,
                            window.chartColors.black,
                            window.chartColors.red,
                            /*colores claros*/
                            window.chartColors.orangeclear,
                            window.chartColors.silverclear,
                            window.chartColors.yellowclear,
                            window.chartColors.greenclear,
                            window.chartColors.turkesaclear,
                            window.chartColors.blueclear,
                            window.chartColors.purpleclear,
                            window.chartColors.greyclear,
                            window.chartColors.blackclear,
                            window.chartColors.redclear
                        ],
                        data: total
                    }
                ]
            };

            var ctx = $("#canvascantidadestadoripsvalidadostodosanios");
            //options
            var option = {

                responsive: true,
                title: {
                    display: true,
                    position: "top",
                    text: "Cantidad RIPS Radicados por años",
                    fontSize: 10,
                },
                legend: {
                    display: true,
                    position: "bottom",
                    labels: {
                        fontSize: 10
                    }
                }

            };


            var barGraph = new Chart(ctx, {
                type: 'pie',
                data: chartdata,
                options: option
            });
        },
        error: function (data) {
            console.log(data);
        }
    });

    ripsvalidadosxestadoswebvalidacion(token);
}


/**
 * Lista la cantidad de estados obtenidos en la validacion por la plataforma web
 */
function ripsvalidadosxestadoswebvalidacion(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarEstadosXAniosWebValidacion',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
            $.each(data, function (k, v) {
                //Si el año es el actual y los valores estan vacios
                if(v.Anio == (new Date()).getFullYear() && v.Cantidad > 0){
                    
                    //envio a la primera tarjeta el valor retornado por la api
                    if(v.Fk_estado == 4){
                        $('#total_rips_anio_actual_errores_estructura').html('Tiene un total de ' + v.Cantidad +' envíos con errores de estructura para el año '+v.Anio+'.')
                    }

                    //Estados.push(v.Estado);

                    //total.push(parseInt(v.Cantidad));
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });
                }else if((v.Anio == (new Date()).getFullYear() && v.Cantidad < 0) && v.Anio == (new Date()).getFullYear()-1 ){ // si no hay valores para el año actual 
                    
                    //envio a la primera tarjeta el valor retornado por la api
                    if(v.Fk_estado == 4){
                        $('#total_rips_anio_actual_errores_estructura').html('Tuvo un total de ' + v.Cantidad +' envíos con errores de estructura para el año '+v.Anio+'.')
                    }
                    

                    Estados.push(v.Estado.toString());
                    //total.push(v.Cantidad);
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });

                }
                //ELIMINAR DUPLICADO DE UN ARRAY
                //if ($.inArray(v.Descripcion_Zona, Zonas) === -1) Zonas.push(v.Descripcion_Zona);
                //Separo los distritos y creo un nuevo array
                //var distr = v.Descripcion_Distrito;
                //var Distritos1 = distr.split(",");
                //Distritos = $.merge( $.merge( [], Distritos ), Distritos1 );

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: ['Pre-Validados'],
                datasets: datos
            }

            var ctx = document.getElementById("canvascantidadestadoripswebvalidados").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
					 scales: {
						yAxes: [{
							ticks: {
								beginAtZero:true
							}
						}]
					},
                    title: {
                        display: true,
                        text: "Envíos para el año " + (new Date()).getFullYear()+" por estado."
                    }, 
                    responsive: true,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    ripsvalidadosxestadoswebpreradicacion(token);
}

/**
 * Lista la cantidad de estados obtenidos en la pre carga por la plataforma web
 */
function ripsvalidadosxestadoswebpreradicacion(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarEstadosXAniosWebPreRadicacion',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
            $.each(data, function (k, v) {
                //Si el año es el actual y los valores estan vacios
                if(v.Anio == (new Date()).getFullYear() && v.Cantidad > 0){
                    
                    //TODO sin uso
                    if(v.Fk_estado == 4){
                     
                    }

                    //Estados.push(v.Estado);

                    //total.push(parseInt(v.Cantidad));
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });
                }else if((v.Anio == (new Date()).getFullYear() && v.Cantidad < 0) && v.Anio == (new Date()).getFullYear()-1 ){ // si no hay valores para el año actual 
                    
                    //TODO sin uso
                    if(v.Fk_estado == 4){
                     
                    }
                    

                    Estados.push(v.Estado.toString());
                    //total.push(v.Cantidad);
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });

                }
                

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: ['Cargados'],
                datasets: datos
            }

            //var ctx = document.getElementById("canvascantidadestadoripsprecargados").getContext("2d");
            var ctx = document.getElementById("").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
					 scales: {
						yAxes: [{
							ticks: {
								beginAtZero:true
							}
						}]
					},
                    title: {
                        display: true,
                        text: "Cantidad RIPS cargados para el año " + (new Date()).getFullYear()+" por estado."
                    }, 
                    responsive: true,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    ripsvalidadosxestadosserviciovalida(token);
}

/**
 * Lista la cantidad de estados obtenidos por el servicio de validacion
 */
function ripsvalidadosxestadosserviciovalida(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarEstadosXAniosServicioValidacion',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
			var rescantidad = 0;
            $.each(data, function (k, v) {
                //Si el año es el actual y los valores estan vacios
                if(v.Anio == (new Date()).getFullYear() && v.Cantidad > 0){
                    
					//envio a la tercera tarjeta el valor retornado por la api
                    if(v.Fk_estado == 8 || v.Fk_estado == 9){
						rescantidad=+v.Cantidad;
                        $('#total_rips_errores_validacion_anio_actual').html('Tiene un total de ' + rescantidad +' envios cargados con errores de validación para el año '+v.Anio+'.')
                    }else{
						//console.log('hola');
                        $('#total_rips_errores_validacion_anio_actual').html('Tiene un total de ' + rescantidad +' envios cargados con errores de validación para el año '+((new Date()).getFullYear())+'.')
					}

                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });
                }else if((v.Anio == (new Date()).getFullYear() && v.Cantidad < 0) && v.Anio == (new Date()).getFullYear()-1 ){ // si no hay valores para el año actual 
                    
                    //envio a la tercera tarjeta el valor retornado por la api
                    if(v.Fk_estado == 8 || v.Fk_estado == 9){
						rescantidad=+v.Cantidad;
                        $('#total_rips_errores_validacion_anio_actual').html('Tuvo un total de ' + rescantidad +' envios cargados con errores de validación para el año '+v.Anio+'.')
                    }else{
						
                        $('#total_rips_errores_validacion_anio_actual').html('Tuvo un total de ' + rescantidad +' envios cargados con errores de validación para el año '+((new Date()).getFullYear()-1)+'.')
					}
                    

                    Estados.push(v.Estado.toString());
                    //total.push(v.Cantidad);
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });

                }
                

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: ['Validados'],
                datasets: datos
            }

            var ctx = document.getElementById("canvascantidadestadoripsserviciovalido").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
					 scales: {
						yAxes: [{
							ticks: {
								beginAtZero:true
							}
						}]
					},
                    title: {
                        display: true,
                        text: "Cantidad RIPS validados para el año " + (new Date()).getFullYear()+" por estado."
                    }, 
                    responsive: true,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    ripsvalidadosxestadosradicados(token);
}

/**
 * Lista la cantidad de estados obtenidos en radicacion
 */
function ripsvalidadosxestadosradicados(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarEstadosXAniosRadicacion',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
			var rescantidad = 0;
            $.each(data, function (k, v) {
                //Si el año es el actual y los valores estan vacios
                if(v.Anio == (new Date()).getFullYear() && v.Cantidad > 0){
                    
                    //envio a la cuarta tarjeta el valor retornado por la api
                    if(v.Fk_estado == 10 || v.Fk_estado == 11 ){
                        rescantidad=+v.Cantidad;
                        $('#total_rips_radicados_anio_actual').html('Tiene un total de ' + rescantidad +' envios radicados para el año '+v.Anio+'.')
                    }else{
						
                        $('#total_rips_radicados_anio_actual').html('Tiene un total de ' + rescantidad +' envios radicados para el año '+((new Date()).getFullYear()-1)+'.')
					}

                    //Estados.push(v.Estado);

                    //total.push(parseInt(v.Cantidad));
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });
                }else if((v.Anio == (new Date()).getFullYear() && v.Cantidad < 0) && v.Anio == (new Date()).getFullYear()-1 ){ // si no hay valores para el año actual 
                    
                    //envio a la cuarta tarjeta el valor retornado por la api
                    if(v.Fk_estado == 10 || v.Fk_estado == 11 ){
                        rescantidad=+v.Cantidad;
                        $('#total_rips_radicados_anio_actual').html('Tuvo un total de ' + rescantidad +' envios radicados para el año '+v.Anio+'.')
                    }else{
						
                        $('#total_rips_radicados_anio_actual').html('Tuvo un total de ' + rescantidad +' envios radicados para el año '+((new Date()).getFullYear()-1)+'.')
					}
                    

                    Estados.push(v.Estado.toString());
                    //total.push(v.Cantidad);
                    if (k == 0) {
                        color = window.chartColors.green
                    } else if (k == 1) {
                        color = window.chartColors.blue
                    } else if (k==2){
                        color=window.chartColors.red
                    }
                    datos.push({
                        label: v.Estado.toString(),
                        backgroundColor: color,
                        fillColor: window.chartColors.blue,
                        strokeColor: color,
                        data: [parseInt(v.Cantidad)]
                    });

                }
                

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: ['Radicados'],
                datasets: datos
            }

            //var ctx = document.getElementById("canvascantidadestadoripsradicados").getContext("2d");
            var ctx = document.getElementById("").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
					 scales: {
						yAxes: [{
							ticks: {
								beginAtZero:true
							}
						}]
					},
                    title: {
                        display: true,
                        text: "Cantidad RIPS radicados para el año " + (new Date()).getFullYear()+" por estado."
                    }, 
                    responsive: true,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    ripscargadosyestadosXaniosXmeses(token);
}


//Pinto grafica preradicados x estados x años x meses
function ripscargadosyestadosXaniosXmeses(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarEstadosXAniosXmes',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
            $.each(data, function (k, v) {
                Estados.push(v.Estado);

                total.push(v.Cantidad);
                if (k == 0) {
                    color = window.chartColors.green
                } else if (k == 1) {
                    color = window.chartColors.blue
                } else if (k==2){
                    color=window.chartColors.red
                }
                datos.push({
                    label: v.Estado.toString(),
                    backgroundColor: color,
                    fillColor: window.chartColors.blue,
                    strokeColor: color,
                    data: [v.Cantidad]
                });

            });
            
            //SECCION PARA ARMAR LA GRAFICA
             var MONTHS = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'];
        var config = {
            type: 'line',
            data: {
                labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'],
                datasets: [{
                    label: 'My First dataset',
                    backgroundColor: window.chartColors.red,
                    borderColor: window.chartColors.red,
                    data: [
                        10,
                        20,                        
                        30						
                    ],
                    fill: false,
                }, {
                    label: 'My Second dataset',
                    fill: false,
                    backgroundColor: window.chartColors.blue,
                    borderColor: window.chartColors.blue,
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor()
                    ],
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Chart.js Line Chart'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Meses'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Cantidad'
                        }
                    }]
                }
            }
        };
            var ctx = document.getElementById("canvascantidadestadoripscargadosxanioxmes").getContext("2d");
			window.myLine = new Chart(ctx, config);
         
            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    
}

//Pinto grafica preradicados x estados
function ripscargadosyestadosPreradicados(token) {

    $.ajax({
        url: baseURL + 'api/Indicadores/ListarXEstadosWebRadicacion',
        method: 'GET',
        data: { iduser: token },
        beforeSend: function () {
        },
        success: function (data) {
            //console.log(data);
            var Estados = [];
            var total = [];
            var datos = [];
            var color ;
            $.each(data, function (k, v) {
                Estados.push(v.Estado);

                total.push(v.Cantidad);
                if (k == 0) {
                    color = window.chartColors.green
                } else if (k == 1) {
                    color = window.chartColors.blue
                } else if (k==2){
                    color=window.chartColors.red
                }
                datos.push({
                    label: v.Estado.toString(),
                    backgroundColor: color,
                    fillColor: window.chartColors.blue,
                    strokeColor: color,
                    data: [v.Cantidad]
                });
                //ELIMINAR DUPLICADO DE UN ARRAY
                //if ($.inArray(v.Descripcion_Zona, Zonas) === -1) Zonas.push(v.Descripcion_Zona);
                //Separo los distritos y creo un nuevo array
                //var distr = v.Descripcion_Distrito;
                //var Distritos1 = distr.split(",");
                //Distritos = $.merge( $.merge( [], Distritos ), Distritos1 );

            });
            
            //SECCION PARA ARMAR LA GRAFICA
            var barChartData = {
                labels: [""],
                datasets: datos
            }

            var ctx = document.getElementById("canvascantidadestadoripscargadosxestadospreradicados").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    title: {
                        display: true,
                        text: "Estado RIPS cargados para el año " + (new Date()).getFullYear()
                    }, 
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Estado'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Cantidad'
                        }
                    }]
                },
                    responsive: false,
                }
            });



            //FIN SECCION PARA ARMAR LA GRAFICA

        }

    });
    
}