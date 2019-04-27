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

function removeCart(product_id){
    console.log(product_id);
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });
    $.ajax({
        data: {'product_id' : product_id},
        type: "POST",
        url: "/store/removeCart",
        success: function(data){
            $('#contents_box').html(data);
        },
        error: function(data){
            console.log(data);
        }
    })
}

$( document ).ready( function() {
    $('#cb').click( function() {
      console.log(11);
      $('.cb').prop( 'checked', this.checked );
    } );
});