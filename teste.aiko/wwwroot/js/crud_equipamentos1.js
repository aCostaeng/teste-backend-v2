const base_url = '/Equipamento/api/v1/';

$(document).ready(function () {
    actualizarTabela();
});

function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: base_url + 'listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaEquipamentos > tbody").empty();
            var tr = '';
            $.each(response, function (i, item) {
                tr += '<tr><td>' + item.nome + '</td><td>' + item.modelo + '</td> <td><button type="button" value=' + item.id + ' class="btn btn-outline-primary btn-sm" onclick="edicao(this);">Editar</button> <button type="button" value=' + item.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaEquipamentos').append(tr);
        },
        error: function (ex) {
        }
    });
}

let IdEquipamento;
function edicao(equipamento) {
    $.ajax({
        type: 'GET',
        url: base_url + equipamento.value,
        contentType: "application/json",
        success: function (data) {
            IdEquipamento = data.id;
            document.getElementById("nomeEquipamento").value = data.name;
            document.getElementById("cmbEquipamentoModelo").value = data.equipmentModelId;
            $('#modal-criar-equipamento').modal('show');
        },
        error: function (ex) {

        }
    })
}


$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoModelo/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEquipamentoModelo").append('<option value="' + "0" + '">' + "Selecione o Modelo" + '</option>');
            $.each(data, function (i, modelo) {
                $("#cmbEquipamentoModelo").append('<option value="' + modelo.id + '">' + modelo.name + '</option>');
            });
        },
        error: function (ex) {

        }
    })
});


$(document).ready(function () {
    $('#btn-modal-equipamento').click(function () {
        $('#modal-criar-equipamento').modal('show')
    });
});

$(document).ready(function () {
    $('#btn-criar-equipamento').click(function () {
        const estado = {
            Nome: document.getElementById("nomeEquipamento").value,
            equipamentoModeloId: document.getElementById("cmbEquipamentoModelo").value
        }
        const estadoJSON = JSON.stringify(estado);
        $.ajax({
            type: 'POST',
            url: '/Equipamento/api/v1/criar',
            data: estadoJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-equipamento').modal('hide')
                actualizarTabela();
            },
            error: function (ex) {
            }
        });
    });
});

$(document).ready(function () {
    $('#btn-editar-equipamento').click(function () {
        const estado = {
            Id: IdEquipamento,
            Nome: document.getElementById("nomeEquipamento").value,
            EquipamentoModeloId: document.getElementById("cmbEquipamentoModelo").value
        };
        const estadoJSON = JSON.stringify(estado);
        $.ajax({
            type: 'POST',
            url: base_url + 'editar',
            data: estadoJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-equipamento').modal('hide');
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
        url: base_url + 'excluir/' + equipamento.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Este equipamento já está a ser utilizado, deve excluir o equipamento antes!");
        }
    })
};

