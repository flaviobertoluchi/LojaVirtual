$(function () {
    $('.endereco').on('change', function () {
        $.get('enderecopartial/' + $(this).val())
            .done(function (response) {
                $(".enderecopartial").html(response);
            });
    });
});
