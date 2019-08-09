<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Session;

class LocaleController extends Controller
{
    public function localeChange(Request $request, $locale){
        if ($locale) {
            \App::setLocale($locale);
            Session::put('locale', $locale);
        }
        return redirect($request->header("referer"));
    }
}