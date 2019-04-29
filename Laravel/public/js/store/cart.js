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

function order(){
    var product_id = [];
    for(let i=0; i<$('.select_2').length; i++){
        product_id.push($('#product_id_'+i).val());
    }
    location.href="/store/orderSheetPage?id="+product_id;
}

$( document ).ready( function() {
    $('#cb').click( function() {
      console.log(11);
      $('.cb').prop( 'checked', this.checked );
    } );
});