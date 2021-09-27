$(document).ready(function () {
    actualizarTabela();
});

function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoEstado/api/v1/listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaEquipamentoEstados > tbody").empty();
            var tr = '';
            $.each(response, function (i, item) {
                tr += '<tr><td>' + item.name + '</td><td>' + item.color + '</td> <td><button type="button" value=' + item.id + ' class="btn btn-outline-primary btn-sm" onclick="editar(this);">Editar</button> <button type="button" value=' + item.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaEquipamentoEstados').append(tr);
        },
        error: function (ex) {
        }
    });
}

function excluir(estado) {
    $.ajax({
        type: 'DELETE',
        url: '/EquipamentoEstado/api/v1/excluir/' + estado.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Este estado já está associado a um equipado, deve excluir o equipamento antes!");
        }
    })
}

function editar(estado) {
    alert("Editar")
    alert(estado.value);
}

$(document).ready(function () {
    $('#btn-abrir-modal').click(function () {
        $('#modal-criar-estado').modal('show')
    });
});

$(document).ready(function () {
    $('#btn-criar-estado').click(function () {
        const estado = {
            Nome: document.getElementById("nome").value,
            Cor: document.getElementById("cor").value
        }
        const estadoJSON = JSON.stringify(estado);
        $.ajax({
            type: 'POST',
            url: '/EquipamentoEstado/api/v1/criar',
            data: estadoJSON,
            contentType: "application/json",
            accepts:"application/json",
            success: function (response) {
                alert("Estado criado com sucesso")
            },
            error: function (ex) {
            }
        });
    });
});

