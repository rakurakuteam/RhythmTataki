console.log('heartJs load');

function heartToggle(id){
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    
    $.ajax({
        data: { 'id': id },
        type: "POST",
        url: "/heartToggle",
        success: function (data) {
            $('.view_like_'+id).html(data);
            // console.log(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}

function download(id){
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    
    $.ajax({
        data: { 'id': id },
        type: "POST",
        url: "/download",
        success: function (data) {
            console.log(data);
            location.reload();
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}