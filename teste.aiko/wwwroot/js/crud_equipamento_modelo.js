$(document).ready(function () {
    actualizarTabela();
});

function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoModelo/api/v1/listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaModelo > tbody").empty();
            var tr = '';
            $.each(response, function (i, item) {
                tr += '<tr><td>' + item.name + '</td><td><button type="button" value=' + item.id + ' class="btn btn-outline-primary btn-sm" onclick="editar(this);">Editar</button> <button type="button" value=' + item.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaModelo').append(tr);
        },
        error: function (ex) {
        }
    });
}

$(document).ready(function () {
    $('#btn-modal-modelo').click(function () {
        $('#modal-criar-modelo').modal('show')
    });
});

$(document).ready(function () {
    $('#btn-criar-modelo').click(function () {
        const modelo = {
            Nome: document.getElementById("nomeModelo").value,
        }
        const modeloJSON = JSON.stringify(modelo);
        $.ajax({
            type: 'POST',
            url: '/EquipamentoModelo/api/v1/criar',
            data: modeloJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-modelo').modal('hide')
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
        url: '/EquipamentoModelo/api/v1/excluir/' + equipamento.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Este modelo já está associado a um equipamentoo, deve excluir o equipamento antes!");
        }
    })
}
