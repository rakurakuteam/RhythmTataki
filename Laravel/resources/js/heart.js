// // console.log('heartJs load');

// var doms = document.getElementsByClassName("heartToggle");
// for (let i = 0; i < doms.length; i++) {
//     doms[i].addEventListener('click', function() {
//         $.ajaxSetup({
//             headers: {
//                 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
//             }
//         });

//         // console.log('heartJs2 click');
//         let id = doms[i].id;

//         $.ajax({
//             data: { 'id': id },
//             type: "POST",
//             url: "/heartToggle",
//             success: function (data) {
//                 console.log(i);
//                 $('#view_like_'+i).html(data);
//             },
//             error: function (data) {
//                 console.log(data.status);
//             }
//         });
//     });
// }