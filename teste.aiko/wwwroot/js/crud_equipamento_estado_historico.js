$(document).ready(function () {
    actualizarTabela();
});

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Equipamento/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEquipamentoEstadoHistorico").append('<option value="' + "0" + '">' + "Selecione o Equipamento" + '</option>');
            $.each(data, function (i, equipamento) {
                $("#cmbEquipamentoEstadoHistorico").append('<option value="' + equipamento.id + '">' + equipamento.nome + '</option>');
            });
        },
        error: function (ex) {

        }
    })
});

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoEstado/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEstadoHistorico").append('<option value="' + "0" + '">' + "Selecione o Estado" + '</option>');
            $.each(data, function (i, equipamento) {
                $("#cmbEstadoHistorico").append('<option value="' + equipamento.id + '">' + equipamento.name + '</option>');
            });
        },
        error: function (ex) {

        }
    })
});


function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoEstadoHistorico/api/v1/listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaEquipamentoEstadoHistorico > tbody").empty();
            var tr = '';
            $.each(response, function (i, item) {
                tr += '<tr><td>' + item.equipamento + '</td><td>' + item.estado + '</td> <td>' + item.data + '</td>  <td><button type="button" value=' + item.id + ' class="btn btn-outline-primary btn-sm" onclick="editar(this);">Editar</button> <button type="button" value=' + item.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaEquipamentoEstadoHistorico').append(tr);
        },
        error: function (ex) {
        }
    });
}

function excluir(estadoHistorico) {
    $.ajax({
        type: 'DELETE',
        url: '/EquipamentoEstadoHistorico/api/v1/excluir/' + estadoHistorico.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Este estado já está associado a um equipado, deve excluir o equipamento antes!");
        }
    })
}

$(document).ready(function () {
    $('#btn-criar-estado-historico').click(function () {
        const estadoHistorico = {
            IdEquipamento: document.getElementById("cmbEquipamentoEstadoHistorico").value,
            IdEstado: document.getElementById("cmbEstadoHistorico").value
        }
        const estadoHistoricoJSON = JSON.stringify(estadoHistorico);
        $.ajax({
            type: 'POST',
            url: '/EquipamentoEstadoHistorico/api/v1/criar',
            data: estadoHistoricoJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-estado-historico').modal('hide')
                actualizarTabela();
            },
            error: function (ex) {
            }
        });
    });
});

function editar(estado) {
    alert("Editar")
    alert(estado.value);
}

$(document).ready(function () {
    $('#btn-abrir-modal-estado-historico').click(function () {
        $('#modal-criar-estado-historico').modal('show')
    });
});