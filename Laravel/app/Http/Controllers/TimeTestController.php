<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Carbon\Carbon;
use App\Chart;
use Illuminate\Support\Facades\DB;

class TimeTestController extends Controller
{
    public function chart(){
        $month = 4;
       
        $date = Carbon::now()->month($month)->lastOfMonth()->format('d');
        $current = Carbon::now()->month($month);
        // $chart['last_date']= $date;

        $chart = Chart::whereMonth('date', $current->format('m'))->select('date', 'u_id', DB::Raw('sum(price) as total_price'))
        ->groupBy('date', 'u_id')->get();

        // ->lastOfMonth()->diffInDays(Carbon::now()->firstOfMonth());
        return response()->json($chart, 200, [], JSON_PRETTY_PRINT);
    }
}
