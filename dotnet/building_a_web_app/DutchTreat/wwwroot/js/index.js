$(document).ready(function(){
    let theForm = $('#theForm');

    theForm.hide();

    let button = $('#buyButton');
    button.on('click', function(){
        console.log('Buying Item');
    });

    let productInfo = $('.product-props li');
    
    productInfo.on('click', function(){
        console.log('You clicked on ' + $(this).text());
    });

    let $logginToggle = $('#loginToggle');
    let $popupForm = $('.popup-form');

    $logginToggle.on('click', function(){
        $popupForm.toggle(100);
    });
});
