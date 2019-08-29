<?php

namespace App\Http\Middleware;
use Illuminate\Support\Facades\Session;
use Closure;

use Illuminate\Support\Facades\Log;

class SetLocale
{
    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \Closure  $next
     * @return mixed
     */
    public function handle($request, Closure $next)
    {
        $locale = Session::get('locale');
        Log::info($locale);
        if($locale) {
            \App::setLocale($locale);
        }
        return $next($request);
    }
}
