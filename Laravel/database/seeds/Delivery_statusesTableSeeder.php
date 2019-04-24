<?php

use Illuminate\Database\Seeder;

class Delivery_statusesTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $status = ["결제완료", "배송준비중", "배송중", "배송완료", "구매확인"];
        for($i=0; $i<count($status); $i++){
            DB::table('delivery_statuses')->insert([
                'status' => $status[$i]
            ]);
        }
    }
}
