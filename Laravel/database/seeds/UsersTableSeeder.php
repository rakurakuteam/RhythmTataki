<?php

use Illuminate\Database\Seeder;

class UsersTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=20; $i++){
            DB::table('users')->insert([
                'name' => "테스터".$i,
                'email' => "tester".$i.'@gmail.com',
                'password' => bcrypt('test'),
                'confirm' => true,
                'created_at' => now(),
            ]);
        }
    }
}
