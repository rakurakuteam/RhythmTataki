console.log('paginationJs load');

function pagination(page){
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });

    console.log(page);
    
    $.ajax({
        data: { 'page': page },
        type: "POST",
        url: "/pagination",
        success: function (data) {
            $('#paging').html(data);
            console.log(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}