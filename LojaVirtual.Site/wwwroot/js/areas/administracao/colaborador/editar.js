$(function () {

    $('#todas').prop('checked', todas());

    $('.permissao').on('change', function () {
        $('#todas').prop('checked', todas());
    });

    $('#todas').on('change', function () {
        if ($(this).is(':checked')) {
            $('.permissao').prop('checked', true);
        }
        else {
            $('.permissao').prop('checked', false);
        }
    });

    function todas() {
        return $('.permissao').not(':checked').length === 0;
    }
});
