$(document).ready(function () {
    $('#formRegistro').validator({
      message: 'Este valor no es valido',
      feedbackIcons: {
        valid: 'glyphicon',
        invalid: 'glyphicon',
        validating: 'glyphicon glyphicon-refresh'
      },
      fields: {
        CodPrestador: {
          validators: {
            notEmpty: {
              message: 'El campo Nacionalidad no puede estar vacío.'
            }
          }
        },
    }
    }).on('success.form.bv', function (e) {
      e.preventDefault();
      guardar();
    });
  });
  
  