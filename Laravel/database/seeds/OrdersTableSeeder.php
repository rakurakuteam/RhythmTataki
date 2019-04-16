<?php

use Illuminate\Database\Seeder;

class OrdersTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=100; $i++){
            DB::table('orders')->insert([
                'order_num' => str_replace('-', '', now()->toDateString()).sprintf("%03d",$i),
                'product_id' => random_int(1, 3),
                'quantity' => 1,
                'user_addr_id' => random_int(1, DB::table('user_addrs')->count()),
                'request' => '요청사항 : '.$i,
                'status_id' => random_int(1, DB::table('delivery_statuses')->count()),
                'payment' => '결제수단 : '.$i,
                'created_at' => now(),
            ]);
        }
    }
}
