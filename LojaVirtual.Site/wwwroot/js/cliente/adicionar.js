$(function () {
    $('.cpfBR').mask('000.000.000-00', { reverse: true });
    $('.phone').mask('(00) 00000-0000', { clearIfNotMatch: true });
    $('.postalcodeBR').mask('00000-000');

    $('#pesquisarCep').on('click', function (e) {
        e.preventDefault();
        let pesquisaCep = $('#pesquisarCep');
        let cep = $('.postalcodeBR').val().replace('-', '');
        if (cep.length != 8) {
            return;
        }

        $.get('https://opencep.com/v1/' + cep)
            .done(function (response) {
                $('#Logradouro').val(response.logradouro);
                $('#Complemento').val(response.complemento);
                $('#Cidade').val(response.localidade);
                $('#Bairro').val(response.bairro);
                $('#Uf').val(response.uf);
                $('#EnderecoNumero').trigger('focus');
                pesquisaCep.popover('hide');
            })
            .fail(function () {
                pesquisaCep.popover('show');
                setTimeout(function () {
                    pesquisaCep.popover('hide');
                }, 5000);
            });
    });
});
