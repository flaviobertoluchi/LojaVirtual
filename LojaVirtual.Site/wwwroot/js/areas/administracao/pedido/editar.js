$(function () {
    $('#tiposituacaopedido').on('change', function () {
        let mensagem = $('#mensagem');
        switch ($(this).val()) {

            case '1':
                mensagem.val('Pagamento recebido, aguardando envio.');
                break;
            case '2':
                mensagem.val('Pedido enviado.');
                break;
            case '3':
                mensagem.val('Pedido entregue.');
                break;
            case '4':
                mensagem.val('Pedido finalizado.');
                break;
            default:
                break;
        }
    });
});
