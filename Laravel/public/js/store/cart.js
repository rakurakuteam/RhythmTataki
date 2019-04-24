console.log('cartJs load');

function addCart(id){
    console.log(id);
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    $.ajax({
        data: {'id' : id},
        type: "POST",
        url: "/store/addCart",
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}