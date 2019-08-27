console.log('packageJs load');

function videoChange(id, e){
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    console.log();
    $.ajax({
        data: { 'id': id },
        type: "POST",
        url: "/videoChange",
        success: function (data) {
            console.log(data);
            $('#video').html(data);
            // $('.list-group-item-primary').removeClass('list-group-item-primary');
            // $(e.target).addClass('list-group-item-primary');
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}