console.log('packageJs load');

function videoChange(id){
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    console.log(id);
    
    $.ajax({
        data: { 'id': id },
        type: "POST",
        url: "/videoChange",
        success: function (data) {
            console.log(data);
            $('#video').html(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}