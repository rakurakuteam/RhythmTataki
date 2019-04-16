<?php

use Illuminate\Database\Seeder;

class ImagesTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $path = [1 => 'https://s3.ap-northeast-2.amazonaws.com/capstone.rhythmtataki.bucket/products/thumbnail/',
                 2 => 'https://s3.ap-northeast-2.amazonaws.com/capstone.rhythmtataki.bucket/products/original/'];

        $name = [1 => 'piano.jpg', 2 => 'guitar.jpg', 3 => 'phone.jpg'];

        for($i=1; $i<3; $i++){
            for($j=1; $j<=3; $j++){
                DB::table('images')->insert([
                    'product_id' => $j,
                    'path' => $path[$i],
                    'name' => $name[$j],
                ]);
            }
        }
    }
}
