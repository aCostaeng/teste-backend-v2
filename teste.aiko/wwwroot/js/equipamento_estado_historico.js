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

$('#cmbEquipamento').change(function () {
    if ($('#cmbEquipamento').val() == 0) {
        $("#tblListaHistorico > tbody").empty();
    }
    else {
        $.ajax({
            type: 'GET',
            url: '/EquipamentoEstadoHistorico/api/v1/listar/' + $('#cmbEquipamento').val(),
            contentType: "application/json",
            success: function (response) {
                $("#tblListaHistorico > tbody").empty();
                var tr = '';
                $.each(response, function (i, item) {
                    tr += '<tr><td>' + item.estado + '</td><td>' + item.data + '</td></tr>';
                });
                $('#tblListaHistorico').append(tr);
            },
            error: function (ex) {
            }
        });
    }
})