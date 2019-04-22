console.log('quantityJs load');

function plus_minus(check){
    let num = $('#quantity').val();
    if(check){
        if(num>1){
            $('#quantity').val(--num);
        }   
    }else{
        $('#quantity').val(++num);
    }
}

function hold_plus_minus(check){
    setInterval(plus_minus(check), 500);
}