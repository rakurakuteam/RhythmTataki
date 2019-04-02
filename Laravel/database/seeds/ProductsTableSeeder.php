<?php

use Illuminate\Database\Seeder;

class ProductsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=3; $i++){
            DB::table('products')->insert([
                'name' => "샘플".$i,
                'price' => $i*$i*5000,
                'stock' => 100,
                'content' => "샘플 내용 : ".$i,
                'created_at' => now(),
            ]);
        }
    }
}
