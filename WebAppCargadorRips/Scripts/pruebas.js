var baseUrl = 'http://localhost:81/gestionpedidos/';

$(document).ready(function () {
    //petiajax();
    cargarVendedores();
});

function cargarVendedores() {

    $("#jsGrid").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        paging: true,
        autoload: true,
        inserting: true,
        editing: true,
        pageSize: 10,
        deleteConfirm: "Esta Seguro de eliminar el registro?",
        //filtering: true,
        controller: {
            loadData: function (filter) {
                var data = $.Deferred();
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: baseUrl + "Vendedores/listarVendedores",
                    dataType: "json"
                }).done(function (response) {
                    data.resolve(response);
                }).fail(function () {
                    alert("error");
                });
                return data.promise();
            },
            insertItem: function (item) {
                return $.ajax({
                    type: "POST",
                    url: "/clients/",
                    data: item
                });
            },
            updateItem: function (item) {
                return $.ajax({
                    type: "PUT",
                    url: "/clients/",
                    data: item
                });
            },
            deleteItem: function (item) {
                return $.ajax({
                    type: "DELETE",
                    url: "/clients/",
                    data: item
                });
            }
        },
        fields: [
            { name: "idvendedor", title: "Codigo", type: "text" },
            { name: "nobres", title: "Nombre", type: "text" },
            { name: "aepellidos", title: "Apellidos", type: "text" },
            { name: "correo", title: "Correo", type: "text" },
            { type: "control" }
        ]
    });

}

function petiajax() {
    $.ajax({
        url: baseUrl + 'Vendedores/listarVendedores',
        method: 'GET',
        //data: {idusuario: idusuario},
        dataType: 'json',
        beforeSend: function () {
            //alert("consultando");
        },
        success: function (data) {
            //tabla.empty();


        }
    });
}
