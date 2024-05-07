$(function () {
    $('.endereco').on('change', function () {
        $.get('enderecopartial/' + $(this).val())
            .done(function (response) {
                $(".enderecopartial").html(response);
            });
    });

    $('#pedidoAlterado').on('change', function () {
        if ($(this).is(':checked')) {
            $('.finalizarPedido').removeAttr('disabled');
        }
        else {
            $('.finalizarPedido').attr('disabled', true);
        }
    });
});
