/**
 * @author ALEXANDER CIFUENTES SANCHEZ
 * @description este archivo se encarga de validar los adjuntos para que el servidor no se cuelgue
 * en esta operación
 * 26-10-2017
 */
//Variable mantiene el modal desplegado
var modalButtonOnly = new tingle.modal({
  closeMethods: [],
  footer: true,
  stickyFooter: true
});

//variable mantiene los errores
var erroresEstructura = []; //new Array(100);
var erroresCaracteres = []; //new Array(100);
//variable mantiene la posicion de lectura
var poslec = 0;
//Variable para buscar caracteres especiales en los archivos
var buscar;
//Variable para buscar puntos y comas (;)  en los archivos
var buscarpc;

$(document).ready(function () {
  var container = document.getElementById('divcontainer');//$('div.container');

  $('select').material_select();

  $.validator.setDefaults({
    ignore: []
  });

  // Extension pour comptabilité avec materialize.css

  $("#formulariocargaarchivo").submit(function (e) {
    e.preventDefault();
  }).validate({
    //debug: true,
    errorClass: "invalid form-error",
    validClass: "valid",
    rules: {
      fechaInicio: {
        required: true,
      },
      fechaFin: "required",
      rips: "required",
    },
    //errorElement : 'div',
    errorContainer: container,
    errorLabelContainer: $("ol", container),
    wrapper: 'li',

    errorPlacement: function (error, element) {
      //error.append(element.parent());
      //console.log(element);

    },
    //For custom messages
    messages: {
      tipoUsuario2: "Seleccione un tipo de usuario",
      categoria: "Seleccione una categoria",
      fechaInicio: "Indique una fecha de inicio",
      fechaFin: "Indique una fecha de fin",
      rips: "Seleccione sus archivos",
    },
    submitHandler: function (e) {
      //readFile();
        terminaLectura();
    }
  });


})

//Función pre lee los archivos antes de cargarlos, con el fin de notificar al usuario
function readFile() {
    
  let bandera = true;//variable buleana bandera que me permitira controlar las validaciones
  var fileInput = document.getElementById('rips');
  var fileDisplayArea = document.getElementById('fileDisplayArea');
  var cantidad = fileInput.childNodes[0].files.length;

  //almaceno el nombre de los archivos en un array para su posterior validacion, ya que no se pueden enviar repetidos


  for (let i = 0; i < cantidad; i++) {
      nombre.push((fileInput.childNodes[0].files[i]['name']).substring(2, 0).toUpperCase());
    }

    //Valido si  los archivos de USUARIOS Y FACTURACIÓN se encuentran para ser cargados
    //ya que son obligatorios
    if (nombre.includes('US') == true && nombre.includes('AF') == true && nombre.length > 2) {

        //Valido que el usuario no seleccione archivos iguales
        //aplico un ordenamiento burbuja para validar que no existan archivos repetidos
        for (let i = 0; i < cantidad - 1; i++) {
            for (let j = i + 1; j < cantidad; j++) {

                if (nombre[i] == nombre[j]) {
                    //Cambio el balor de la variable de validación
                    bandera = false;
                }
            }
        }

        //valido que la variable de validacion no cambio
        if (bandera === true) {
            modalprogres();
            // si la variable bandera no cambio envio lectura de archivos
            for (let i = 0; i < cantidad; i++) {

                var file = fileInput.childNodes[0].files[i];
                var textType = /text.*/;
                //var buscarpuntos=null;
                var namefile = null;
                if (file.type.match(textType)) { //si los archivos no son de formato txt no los permite leer
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        namefile = fileInput.childNodes[0].files[i]['name'];

                        // Por lineas
                        var lines = this.result.split('\n');

                        //envio a una función las lienas del archivo a subir
                        readlines(lines, namefile, cantidad);

                    }
                    reader.readAsText(file);
                    //delete reader;
                } else {
                    swal(
                        'Precaución',
                        'Parece que intenta cargar archivos ilegibles, por favor elimine e intente nuevamente',
                        'info'
                    )
                    //fileDisplayArea.innerText = "Archivos No Soportados!"
                    nombre.length = 0; //Limpio el vector de nombres
                }
            }//fin for
            //cuando termina de leer todos los archivos llamo funcion para que realice las operaciones siguentes
            //terminaLectura();
        } else {
            //si cambio la variable bandera
            //evito la carga innesaria de los archivos
            //le indico al usuario que por favor revice la info a cargar
            swal(
                'Precaución',
                'Parece que intenta cargar el mismo tipo de estructura, por favor elimine e intente nuevamente',
                'info'
            )
            nombre.length = 0;
            nombre = [];
            //limpiarCampos();
        }


    } else { //de lo contrario envio alerta para obligar cargar los archivos
        swal(
            'Precaución',
            'No se encuentran las estructuras de USUARIOS (US) y/o FACTURACIÓN(AF), o le hace falta una estructura de atención, por favor elimine los archivos e intente cargar nuevamente.',
            'info'
        )
        nombre = [];//Limpio el vector de nombres

    } //fin else archivos obligatorios 

  

}

//Función lee linea por linea los archivos antes de cargarlos
function readlines(lineas, namefile, cantidad) {

    //extraigo los nombres
    var nombrecorto = namefile.substring(2, 0).toUpperCase();
    
    //valido que el nombre del archivo sea el permitido de las estructuras definidas en la norma
    if (TipoEstructuraArray[nombrecorto] === undefined && nombrecorto !== 'CT' && nombrecorto !== 'AD') {
        modalButtonOnly.close();
        //limpio variable de posicion de lectura
        poslec = 0;
        swal(
            'Error!',
            'Esta intentando cargar estructuras con un nombre no permitido, por favor corrijalos e intente nuevamente!',
            'error'
        )
    }else {
        
        /*
         # valido si hay caracteres especiales
         # y el archivo es diferente a la estrutura AM envio errores
         */
        if (nombrecorto !== 'CT' && nombrecorto !== 'AD') //validación para Procedimientos
        {
           buscarpc = new RegExp(/;/); //buscar caracteres especiales            
        }
        if (nombrecorto !== 'CT' && nombrecorto !== 'AD'  && nombrecorto !== 'AM' && nombrecorto !== 'AT' && nombrecorto !== 'AP' && nombrecorto !== 'AU')
        {
            buscar = new RegExp(/[~`!#$%;\^&*+=\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales
        }
        else if (nombrecorto == 'AM') //validación para Medicamentos
        {
            buscar = new RegExp(/[~`!#$;\^&\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        }
        /*else if (nombrecorto == 'AT') //validación para Otros Servicios
        {
            buscar = new RegExp(/[~`!$;\^&\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        } */
        else if (nombrecorto == 'AP') //validación para Procedimientos
        {
            buscar = new RegExp(/[~`!#$%;\^&*\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales            
        }
        
        //var pattern="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"; //buscar fecha
        //var pattern2 =/^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/; //para validar el formato de la fecha

        //valido liena por linea que no tenga caracteres especiales
        $.each(lineas, function (i, v) {
            //console.log(v.replace("\r",""));           

            //busco si los archivos tiene puntos y comas
            var puntocoma = false;

            //busco si el valor trae un dato de bool
            var res = false;

            if (buscar === undefined) {
                puntocoma = false;
                res = false;
            }
            else {
                puntocoma = buscarpc.test(v);
                res = buscar.test(v);
            }
            
            textoAreaDividido = v.split(",");
            numeroColumas = textoAreaDividido.length;

            /**
             * SE TIENE EN CUENTA QUE EL ARCHIVO CT NO LO ENVIAN MUCHOS PRESTADORES POR LO TANTO SE EXCLUYE DE LA
             * POSTERIRO VALIDACIÓN
             * 28/12/2017
             * EL VALIDADOR EXTRAE SU PROPIA VALIDACIÓN
             */
                //valido la cantidad de campos permitidos para cada estructura
            if (numeroColumas > 1 || puntocoma == true) {
                if (nombrecorto !== 'CT' && puntocoma == false && TipoEstructuraArray[nombrecorto] !== numeroColumas && TipoEstructuraArray[nombrecorto] > 0) {
                        erroresEstructura.push("ERROR: La estructura " + namefile + " en la linea " + (i + 1) + " tiene " + numeroColumas + " campos, la cantidad correcta de campos es de " + TipoEstructuraArray[nombrecorto]);
                }
                if (nombrecorto !== 'CT' && puntocoma == true && TipoEstructuraArray[nombrecorto] !== numeroColumas && TipoEstructuraArray[nombrecorto] > 0) {
                    erroresEstructura.push("ERROR: La estructura " + namefile + " en la linea " + (i + 1) + " tiene el caracter punto y coma (;), elimine el caracter.");
                }
            }
            //fin valido la cantidad de campos permitidos para cada estructura

            /*
               # valido si hay caracteres especiales
               # y el archivo es diferente a la estrutura AM envio errores
           */
            if (res == true && nombrecorto !== 'CT' && nombrecorto !== 'AT') {
                erroresCaracteres.push("error" + (i + 1) + ": La estructura " + namefile + " contiene valores no permitidos en la linea " + (i + 1));
            }

        });


        //valido liena por linea encontrar codigo de prestador diferente, y solo notifico
        $.each(lineas, function (i, v) {

            buscar = undefined; //limpio la variable

            textoAreaDividido = v.split(",");
            numeroColumas = textoAreaDividido.length;

            if (nombrecorto == 'AF') {
                var codprestador = "'" + $('#codigospan').text().replace(/ /g, "")+"'";
                var codprestador2 = "'"+v.split(",", 1)+"'";
                var fechainicio1 = document.getElementById('fechaInicio').value;//$('#fechaInicio') valor;
                var fechafin1 = document.getElementById('fechaFin').value;//$('#fechaFin') valor;
                
                //Valido que la fechas de periodo de inicio seleccionado concuerden con el de los archivos de AF
                if (v.indexOf(fechainicio1.toString(10), 7) == -1) {

                    iziToast.warning({
                        title: 'Alerta',
                        message: 'Sus estructuras presentan una fecha de inicio de reporte diferente, al periodo indicado en el formulario, estructura AF',
                        position: 'topCenter',
                        timeout: 50000,
                        resetOnHover: true,
                        drag: true,
                        close: true,
                    });
                    return false;
                }

                //Valido que la fechas de periodo de fin seleccionado concuerden con el de los archivos de AF
                if (v.indexOf(fechafin1.toString(10), 8) == -1) {

                    iziToast.warning({
                        title: 'Alerta',
                        message: 'Sus estructuras presentan una fecha de fin de reporte diferente, al periodo indicado en el formulario, estructura AF',
                        position: 'topCenter',
                        timeout: 50000,
                        resetOnHover: true,
                        drag: true,
                        close: true,
                    });
                    return false;
                }


                if (codprestador.localeCompare(codprestador2) != 0) {
                    
                    iziToast.warning({
                        title: 'Alerta',
                        message: 'Sus estructuras presentan un código de prestador diferente, al identificado en la plataforma.',
                        position: 'topCenter',
                        timeout: 50000,
                        resetOnHover: true,
                        drag: true,
                        close: true,
                    });
                    return false;
                }

                //console.log("numc" + codprestador + "-");

            }

        });

        //Fin valido liena por linea encontrar codigo de prestador diferente, y solo notifico

        /**
         * ESTA LINEA SE ELIMINA POR RENDIMIENTO DEL APLICATIVO
         * La linea permite que cada linea del archivo se fraccione en un vector para su posterior lectura
         */
        /*for(var pos = 0; pos < lineas.length; pos++){
            var lines2 =lineas[pos].replace("\r","").split(',');
            console.log(lineas[pos]);
            //console.log(typeof lines2[pos]);
            if (lines2[pos] instanceof Date)  
            {  
              //console.log("es fecha");
              //console.log(lines2[pos]);
            } 
            
        }*/
        poslec = poslec + 1; //sumo la posición de lectura

        //valido que se hayan terminado de leer todos los archivos que adjunto el usuario
        if (poslec === cantidad) {
            //cierro el modal 
            modalButtonOnly.close();
            //Llamo funcion de terminacion de lectura
            //terminaLectura();
            //limpio variable de posicion de lectura
            poslec = 0;
        }
    }
}

function modalprogres() {

  modalButtonOnly.open();
  modalButtonOnly.setContent(document.querySelector('.tingle_addon_window').innerHTML);

}

function terminaLectura() {
    //valido que si se presentan errores en la validacion
    //console.log(erroresEstructura);
    if (erroresEstructura.length > 0) {
        //envio notificación del error
        swal({
            title: 'Error',
            text: 'Sus estructuras presentan Errores de estructura y/o divisiones por punto y coma (;) !! se enviara un correo con los diferentes ' +
                'errores encontrados, por favor corrijalos e intente nuevamente.',
            type: 'warning',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK!',
            allowOutsideClick: false
        }).then((result) => {
            //if (result.value) {
            //envio a la api de errores
            UploadValidacionConErrores(erroresEstructura.slice(0, 100));
            //con slice envio del primer error al 100 para no saturar el servidor de correo
            //enviarCorreoErrores(errores.slice(0,100));
            //}
        })

    }else if (erroresCaracteres.length > 0) { //
        //envio notificación de posible error
        swal({
            title: 'Error',
            text: 'Sus estructuras presentan caracteres especiales no permitidos, desea continuar con el cargue, o desea recibir un correo con los diferentes ' +
                'caracteres encontrados.',
            type: 'warning',
            showCancelButton: true,
            cancelButtonText: 'Enviar correo',
            cancelButtonColor: '#d33',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Continuar',
            allowOutsideClick: false
        }).then(function (result) {
            //console.log(result);
            if (result) {
                //llamo funcio de carga de los archivos RIPS
                loadRIPS();
            }
        }, function (dismiss) {
            //console.log(dismiss);
            if (dismiss === 'cancel') { // you might also handle 'close' or 'timer' if you used those
                //envio a la api de errores
                enviarCorreoErrores(erroresCaracteres.slice(0, 100));
            } else {
                throw dismiss;
            }
        })

    } else if (nombre.includes("US") == true && nombre.includes("AF") == true && nombre.length > 2) {
     //console.log('pasa');
    //llamo funcio de carga de los 
    loadRIPS();
  }
}