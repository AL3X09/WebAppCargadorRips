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
var errores = []; //new Array(100);
//variable mantiene la posicion de lectura
var poslec = 0;

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
      readFile();
    }
  });


})

function readFile() {
  //console.log("leiendoXD");
  let bandera = true;//variable buleana bandera que me permitira controlar las validaciones
  var fileInput = document.getElementById('rips');
  var fileDisplayArea = document.getElementById('fileDisplayArea');
  var cantidad = fileInput.childNodes[0].files.length;

  //almaceno el nombre de los archivos en un array para su posterior validacion, ya que no se pueden enviar repetidos


  for (let i = 0; i < cantidad; i++) {
    nombre.push((fileInput.childNodes[0].files[i]['name']).substring(2, 0));
  }
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
          'Parece que intenta cargar archivos no ilegibles, por favor elimine e intente nuevamente',
          'info'
        )
        //fileDisplayArea.innerText = "Archivos No Soportados!"
        nombre.length = 0;
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
      'Parece que intenta cargar el mismo tipo de archivo, por favor elimine e intente nuevamente',
      'info'
    )
    nombre.length = 0;
    //limpiarCampos();
  }

}

function readlines(lineas, namefile, cantidad) {
    var nombrecorto = namefile.substring(2, 0).toUpperCase();

    //valido que el nombre del archivo sea el permitido de las estructuras definidas en la norma
    if (TipoEstructuraArray[nombrecorto] === undefined && nombrecorto !== 'CT') {
        modalButtonOnly.close();
        //limpio variable de posicion de lectura
        poslec = 0;
        swal(
            'Error!',
            'Esta intentando cargar archivos con un nombre no permitido, por favor corrijalos!',
            'error'
        )
    } else {
        var buscar = new RegExp(/[~`!#$%;\^&*+=\[\]\\'{}|\\"<>\?]/); //buscar caracteres especiales
        
        //var pattern="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"; //buscar fecha
        //var pattern2 =/^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/; //para validar el formato de la fecha

        //valido liena por linea que no tenga caracteres especiales
        $.each(lineas, function (i, v) {
            //console.log(v.replace("\r",""));

            var res = buscar.test(v);
            //si hay caracteres especiales envio errores
            if (res == true) {
                errores.push("error" + (i + 1) + " El Archivo " + namefile + " contiene valores no permitidos en la linea " + (i + 1));
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
                if (numeroColumas > 1) {
                    if (nombrecorto !== 'CT' && TipoEstructuraArray[nombrecorto] !== numeroColumas && TipoEstructuraArray[nombrecorto] > 0) {
                        errores.push("ERROR; El archivo " + namefile + " en la linea " + (i + 1) + " tiene una longitud de " + numeroColumas + ", la lingitud permitida es de " + TipoEstructuraArray[nombrecorto]);
                    }
                }
            //fin valido la cantidad de campos permitidos para cada estructura
        });

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
            terminaLectura();
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
  //console.log(errores);
  if (errores.length > 0) {
    //envio notificación del error
    swal({
      title: 'Error',
      text: 'Sus archivos presentan Errores de estructura!! se enviara un correo con los diferentes ' +
      'errores encontrados, por favor corrijalos eh intente nuevamente.',
      type: 'warning',
      showCancelButton: false,
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'OK!',
      allowOutsideClick: false
    }).then((result) => {
      //if (result.value) {
        //envio a la api de errores
        UploadValidacionConErrores(errores.slice(0,100));
        //con slice envio del primer error al 100 para no saturar el servidor de correo
        //enviarCorreoErrores(errores.slice(0,100));
      //}
    })
    //llamo funcion para enviar correo
    //enviarCorreoErrores(errores);
  } else {
    //llamo funcio de carga de los 
    loadRIPS();
  }
}