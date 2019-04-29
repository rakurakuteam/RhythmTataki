console.log('quantityJs load');

// 수량 카운트 && 가격 표시
function quantity(e){
    let prices = price();
    let target_id = e.target.id;
    let id = target_id.split('_');
    let target_name = id[0];
    let target_num = Number(id[id.length-1]);
    let quantity = [];
    
    quantity[target_num] = $('#quantity_'+target_num).val();
    
    console.log(target_num);
    console.log(prices);
    console.log(quantity[target_num]);
    let delivery = $('.price_2').text();
    
    delivery = delivery.replace(',','');
    delivery = Number(delivery.replace('원',''));
    // console.log($('#price_2').val());
    if(target_name == 'minus'){
        if(quantity[target_num]*prices>1){
            $('#quantity_'+target_num).val(--quantity[target_num]);
        }
    }else{
        $('#quantity_'+target_num).val(++quantity[target_num]);
    }
    $('.price_1').text(numberWithCommas(prices*quantity[target_num])+"원");
    if(quantity*1 < 30000){
        delivery = 2500;
        $('.price_2').text("2,500원");
        $('.price_3').text(numberWithCommas(prices*quantity[target_num]+delivery)+"원");
    }else{
        $('.price_2').text("0원");
        $('.price_3').text(numberWithCommas(prices*quantity[target_num])+"원");
    }
    let total_price = price2();
    $('.price_1').text(numberWithCommas(total_price)+"원");
    $('.price').text(numberWithCommas(total_price)+"원");
}

// 개별 금액 계산
function price(){
    var price = $('.price_text').text();
    price = price.replace(',', '');
    price = price.replace('원', '')

    return Number(price);
}

// 장바구니 합계 계산
function price2(){
    var total_price = 0;
    for(var i=0; i<$('.quantity').length; i++){
        // console.log(i);
        // $('#quantity_').val();
        // $('#price_text_').text();
        let quantity = $('#quantity_'+i).val();
        let price = $('#price_text_'+i).text();
        price = price.replace(',','');
        price = Number(price.replace('원',''));
        // console.log($('#quantity_'+i).val())
        // console.log($('#price_text_'+i).text())
        total_price += quantity*price;
        // console.log(total_price);
    }
    console.log(total_price);
    return Number(total_price);
}

// 3자리 콤마
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}