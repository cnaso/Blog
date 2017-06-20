$(document).ready(function () {
    $('.delete').click(function (e) {
        e.preventDefault();

        var $this = $(this);
        var id = $this.data("id");
        var title = $this.data("title");

        $('.dialog').load("../Content/Shared/DeleteDialog.html", function (response, status) {
            if (status == "success") {
                $("#delete-dialog").modal("show");
                $("#delete-dialog").modal().find(".modal-title").html(title);

                $('.confirm-delete').click(function () {
                    $.ajax({
                        type: "post",
                        url: "/Admin/Users/Delete/",
                        ajaxasync: true,
                        data: { id: id },
                        success: function () {
                            window.location.replace("/Admin/Users/Index");
                        },
                        error: function (data) {
                            alert(data.x);
                        }
                    });
                });
            }
        });
    });
});