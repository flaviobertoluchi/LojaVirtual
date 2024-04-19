$(function () {
    $('.cpfBR').mask('000.000.000-00', { reverse: true });
    $('.phone').mask('(00) 00000-0000');
    $('.postalcodeBR').mask('00000-000');

    $('#pesquisarCep').on('click', function (e) {
        e.preventDefault();
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
                $('#pesquisarCep').popover('hide');
            })
            .fail(function () {
                $('#pesquisarCep').popover('show');
                setTimeout(function () {
                    $('#pesquisarCep').popover('hide');
                }, 5000);
            });
    });
});
