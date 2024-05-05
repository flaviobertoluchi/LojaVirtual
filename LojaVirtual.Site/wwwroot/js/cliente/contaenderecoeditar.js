$(function () {
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
                $('#Numero').trigger('focus');
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
