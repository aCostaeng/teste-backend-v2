$(document).ready(function () {
    actualizarTabela();
});

function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoModeloEstadoHora/api/v1/listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaModeloEstadoHora > tbody").empty();
            var tr = '';
            $.each(response, function (i, item) {
                tr += '<tr><td>' + item.modelo + '</td> <td>' + item.estado + '</td>   <td>' + item.valor + '</td> <td><button type="button" value=' + item.id + ' class="btn btn-outline-primary btn-sm" onclick="editar(this);">Editar</button> <button type="button" value=' + item.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaModeloEstadoHora').append(tr);
        },
        error: function (ex) {
        }
    });
}

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoEstado/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEquipamentoEstadoHora").append('<option value="' + "0" + '">' + "Selecione o Equipamento" + '</option>');
            $.each(data, function (i, equipamento) {
                $("#cmbEquipamentoEstadoHora").append('<option value="' + equipamento.id + '">' + equipamento.name + '</option>');
            });
        },
        error: function (ex) {

        }
    })
});

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoModelo/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEquipamentoModelo").append('<option value="' + "0" + '">' + "Selecione o Estado" + '</option>');
            $.each(data, function (i, modelo) {
                $("#cmbEquipamentoModelo").append('<option value="' + modelo.id + '">' + modelo.name + '</option>');
            });
        },
        error: function (ex) {

        }
    })
});

$(document).ready(function () {
    $('#btn-abrir-modal-estado-hora').click(function () {
        $('#modal-criar-estado-hora').modal('show')
    });
});

$(document).ready(function () {
    $('#btn-criar-estado-hora').click(function () {
        const modeloEstadoHora = {
            IdModelo: document.getElementById("cmbEquipamentoModelo").value,
            IdEstado: document.getElementById("cmbEquipamentoEstadoHora").value,
            Valor: document.getElementById("valor").value,
        }
        const modelomodeloEstadoHoraJSON = JSON.stringify(modeloEstadoHora);
        console.log(modelomodeloEstadoHoraJSON)
        $.ajax({
            type: 'POST',
            url: '/EquipamentoModeloEstadoHora/api/v1/criar',
            data: modelomodeloEstadoHoraJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-estado-hora').modal('hide')
                actualizarTabela();
            },
            error: function (ex) {
            }
        });
    });
});

function excluir(equipamento) {
    $.ajax({
        type: 'DELETE',
        url: '/EquipamentoModeloEstadoHora/api/v1/excluir/' + equipamento.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Este modelo já está associado a um equipamentoo, deve excluir o equipamento antes!");
        }
    })
}