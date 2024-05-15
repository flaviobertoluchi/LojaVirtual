$(function () {
    mensagem();

    $('#tiposituacaopedido').on('change', function () {
        mensagem();
    });

    function mensagem() {
        let mensagem = $('#mensagem');
        switch ($('#tiposituacaopedido').val()) {

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
                mensagem.val('');
                break;
        }
    }
});
