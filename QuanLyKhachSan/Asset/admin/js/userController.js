var user = {
    init: function() {

    },
    registerEvents: function () {
        $('.btn-active').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');

            console.log('Hello');

            $.ajax({
                url: '/Admin/Account/ChangeStatus',
                type: 'POST',
                dataType: 'json',
                data: { id: id },
                contentType: 'application/json; charset=utf-8',
                success: function (respone) {
                    if (respone.status == true) {
                        btn.text('Kích hoạt')
                    } else {
                        btn.text('Khóa')
                    }
                },
                error: function (event, request, settings) {
                    alert("Có lỗi xãy ra, vui lòng tải lại trang");
                }
            })
        })
    }
}
user.init();