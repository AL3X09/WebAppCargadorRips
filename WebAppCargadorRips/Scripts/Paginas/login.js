var token = sessionStorage.getItem('key');
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {

    $('#modalLRForm').modal("show");

    $('#Email').bind('cut copy paste', function(event){
        event.preventDefualt();
    });
    //cambio el valor del checkbox ya que el framework no lo reconoce
    $('#RememberMe').change(function(){
       
        if ($(this).is(':checked')) {
             $(this).val('true');
        }else{
            
             $(this).val('false');
        }
    });
    //verifico cambio check terminos
    $('#aceptarTerminos').prop('checked', true); 
    $('#aceptarTerminos').val('true'); 
    //$('#aceptarTerminos').on('click', function () { 
    //});
    //$('#aceptarTerminos').click();

    $('#aceptarTerminos').change(function () {
        
        if ($(this).is(':checked')) {
            $(this).val('true');
            $('#btnregistrarce').prop('disabled', false);
        } else {
            $(this).val('false');
            $('#btnregistrarce').prop('disabled', true);
            }
    });

    $('#modalRecuperarContra').modal("show");
    $('#modalRecuperarContra').modal({
        backdrop: 'static',
        keyboard: false
    });
    
    

});

//NO SE USA POR AHORA valido que el codigo registrado exista antes con completar el formulario
function validarCodPrestador() {
    swal({
        title:'Validando....',
        text: 'validado que el codigo ingresado se encuentre activo por favor espere!',
        onOpen: function(){
            swal.showLoading()
            //llamo ajax
        },
        allowOutsideClick:false
    }).then(
        //function
    )
}

function register() {
    $.ajax({
        type: "POST",
        url: "/any/",
        data: $('#formRegistro').serialize(),
        success: function (response) {
            alert('registro completo');
        },
        error: function (e) {
            console.log(e);
         }
    });
}
//Función que oculta el panel numero 1 para que los datos no se traspongan
function cerrarpanel1() {
    $('#panel1').removeClass("active show");
    $('#panel1').attr("aria-expanded","false");
}
//Función que oculta el panel numero 2 para que los datos no se traspongan
function cerrarpanel2() {
    //console.log('llama función cerrarpanel2')
    $('#panel2').removeClass("active show");
    $('#panel2').attr("aria-expanded", "false");
    
 }
// función usada alertas
function ShowAlert(tipo, msj) {
    debugger;
    var toast ="iziToast."+tipo+"({"+
        "timeout: 20000,"+
        "title: '" + tipo.toUpperCase() +"'," +
        "message: '"+msj+"'," +
        "position: 'topCenter'," +
        "})";
    
    eval(toast);
    
}

//cambio el valor del checkbox ya que el framework no lo reconoce
function terminosCheck() {
    
    $('#aceptarTerminos').change(function () {
        if ($(this).val() == "on") {
            $('#btnregistrarce').prop('disabled', false);
            $(this).val('true');
        } else {
            $('#btnregistrarce').prop('disabled',true);
            $(this).val('false');
        }
    });
}
