<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/
// Route::get('/{any}', 'SinglePageController@index')->where('any', '.*');
Route::get('/', function(){
    return view('master');
});

Route::prefix('login')->group(function () {
    Route::get('google', 'GoogleLoginController@googleLogin')->name('google.login');
    Route::get('google/callback', 'GoogleLoginController@googleCallback')->name('google.callback');
});
Auth::routes();

Route::get('/home', 'HomeController@index')->name('home');

Route::get('/workshop', 'WorkshopController@index')->name('workshop.main');
Route::post('/workshop/upload', 'WorkshopController@upload')->name('workshop.upload');
Route::post('/workshop/cutter', 'WorkshopController@cutter')->name('workshop.cutter');

