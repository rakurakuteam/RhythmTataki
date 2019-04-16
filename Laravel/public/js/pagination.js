console.log('paginationJs load');

function pagination(page){
    let sort = $('#sort').val();

    console.log(page+" / "+sort);

    $.ajax({
        data: { 'page' : page,
                'sort' : sort},
        type: "GET",
        url: "/pagination",
        success: function (data) {
            $('#paging').html(data);
            // console.log(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}