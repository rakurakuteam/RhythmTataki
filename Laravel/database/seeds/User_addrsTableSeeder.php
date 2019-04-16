<?php

use Illuminate\Database\Seeder;

class User_addrsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=DB::table('users')->count(); $i++){
            $rand = random_int(1, 4);
            for($j=0; $j<$rand; $j++){
                DB::table('user_addrs')->insert([
                    'user_id' => $i,
                    'addr_id' => random_int(1, DB::table('addresses')->count()),
                ]);
            };
        };
    }
}