console.log('quantityJs load');

function quantity(e){
    let target_id = e.target.id;
    let id = target_id.split('_');
    let target_name = id[0];
    let target_num = id[id.length-1];
    let quantity = $('#quantity_'+target_num).val();
    let prices = $('#price_text_'+target_num).text();
    // console.log(prices);
    if(target_name == 'minus'){
        if(quantity>1){
            $('#quantity_'+target_num).val(--quantity);
        }
    }else{
        $('#quantity_'+target_num).val(++quantity);
    }
    price2();
    $('.price_1').text(price()*quantity+"원");
    $('.price_4').text("합계"+price2()+"원");
    // $('.price_4').text("합계"+price()*quantity+"원");
}

function price(){
    var price = $('.price_text').text();
    price = price.replace(',', '');
    price = price.replace('원', '')

    return Number(price);
}

function price2(){
    var total_price = 0;
    for(var i=0; i<$('.price_box').length; i++){
        // console.log(i);
        // $('#quantity_').val();
        // $('#price_text_').text();
        let quantity = $('#quantity_'+i).val();
        let price = $('#price_text_'+i).text();
        // console.log($('#quantity_'+i).val())
        // console.log($('#price_text_'+i).text())
        total_price += quantity*price;
        // console.log(total_price);
    }
    console.log(total_price);
    return Number(total_price);
}