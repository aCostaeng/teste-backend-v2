$(document).ready(function () {
    actualizarTabela();
});

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Equipamento/api/v1/listar',
        contentType: "application/json",
        success: function (data) {
            $("#cmbEquipamento").append('<option value="' + "0" + '">' + "Selecione o Equipamento" + '</option>');
            $.each(data, function (i, equipamento) {
                $("#cmbEquipamento").append('<option value="' + equipamento.id + '">' + equipamento.nome + '</option>');
            });
        },
        error: function (ex) {
        }
    })
});

function actualizarTabela() {
    $.ajax({
        type: 'GET',
        url: '/EquipamentoPosicoesHistorico/listar',
        contentType: "application/json",
        success: function (response) {
            $("#tblListaEquipamentoPosicao > tbody").empty();
            var tr = '';
            $.each(response, function (i, historico) {
                tr += '<tr><td>' + historico.equipamento + '</td><td>' + historico.data + '</td> <td>' + historico.lat + '</td> <td>' + historico.lon + '</td> <td><button type="button" value=' + historico.id + ' class="btn btn-outline-primary btn-sm" onclick="editar(this);">Editar</button> <button type="button" value=' + historico.id + ' class="btn btn-outline-danger btn-sm" onclick="excluir(this);">Excluir</button></td></tr>';
            });
            $('#tblListaEquipamentoPosicao').append(tr);
        },
        error: function (ex) {
        }
    });
}

function excluir(estadoHistorico) {
    $.ajax({
        type: 'DELETE',
        url: '/EquipamentoPosicoesHistorico/api/v1/excluir/' + estadoHistorico.value,
        contentType: "application/json",
        success: function (response) {
            actualizarTabela();
        },
        error: function (ex) {
            alert("Falha ao remover histórico de posição do equipamento!");
        }
    })
}

$(document).ready(function () {
    $('#btn-criar-equipamento-posicao').click(function () {
        const historicoPosicao = {
            idEquipamnto: document.getElementById("cmbEquipamentoEstadoHistorico").value,
            lat: document.getElementById("lat").value,
            lon: document.getElementById("lon").value,
            data: document.getElementById("data").value
        }
        const historicoPosicaoJSON = JSON.stringify(historicoPosicao);
        $.ajax({
            type: 'POST',
            url: '/EquipamentoPosicoesHistorico/api/v1/criar',
            data: historicoPosicaoJSON,
            contentType: "application/json",
            accepts: "application/json",
            success: function (response) {
                $('#modal-criar-equipamento-posicao').modal('hide')
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
    $('#btn-abrir-modal-posicao').click(function () {
        $('#modal-criar-equipamento-posicao').modal('show')
    });
});