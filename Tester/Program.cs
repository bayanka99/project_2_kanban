using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Tester
{
    class Program
    {
        public static void Main(string[] args)
        {
            Service service = new Service();

            Response res = service.LoadData();
            service.DeleteData();
            service.LoadData();
            Console.ReadLine();
            //Response res = service.Register("user@gmail.com", "Pass1");
            //res = service.Register("user2@gmail.com", "Pas1");
            //res = service.Register("", "Pas1");

            //res = service.Login("user@gmail.com", "Pas");
            //res = service.Login("user2@gmail.com", "Pass1");

            //res = service.Logout("user@gmail.com");

            //res = service.Register("user@gmail.com", "Pass12");
            //res = service.Login("user@gmail.com", "Pass1");
            //res = service.Login("user2@gmail.com", "Pass1");

            //Console.WriteLine("********************");
            //res = service.AddBoard("user@gmail.com", "");
            //res = service.AddBoard("user@gmail.com", "12345");
            //res = service.AddBoard("user@gmail.com", "12345");
            //res = service.AddBoard("user2@gmail.com", "12345");
            //res = service.RemoveBoard("user2@gmail.com", "12345");
            //res = service.RemoveBoard("user2@gmail.com", "12345");

            //res = service.AddBoard("user2@gmail.com", "12345");

            //res = service.AddTask("user@gmail.com", "12345","title","desc", DateTime.Now.AddDays(1));
            //res = service.AddTask("user@gmail.com", "12345", "title", "desc", DateTime.Now.AddDays(1));


            //res = service.GetColumn("user@gmail.com", "12345", 0);
            //Console.WriteLine("*************************");
            //Response<IList<Task>> rescast = (Response<IList<Task>>)res;
            //foreach (Task t in rescast.Value)
            //{
            //    Console.WriteLine(t.Title);
            //}
            //Console.WriteLine(rescast.Value.ToString());
            //Response res3 = service.Logout("user@gmail.com");
            //Response res22 = service.Login("user@gmail.com", "Pass1");
            //Response res4 = service.AddBoard("user@gmail.com","board1");
            //Response res44 = service.AddTask("user@gmail.com", "board1", "title11", "decp11",DateTime.Now.addDays(1));
            //Response res444 = service.AdvanceTask("user@gmail.com", "board1",1,0);
            //if (res444.ErrorOccured)
            //{
            //    Console.WriteLine(res444.ErrorMessage);
            //}
            //else
            //{
            //    Console.WriteLine("Created user successfully");
            //}
            //Response res5 = service.RemoveBoard("user@gmail.com", "board1");
            //Response res6 = service.RemoveBoard("user@gmail.com", "board1");
            //Console.WriteLine("Hello World!");

            //*/



            ///////bayan tests

            /*
            Response res = service.LoadData();
            Console.WriteLine("test1");
             res= service.Register("user@gmail.com", "Passw0rd");
            Console.WriteLine();
            Console.WriteLine("test2");
            res = service.AddBoard("user@gmail.com", "board11");
            Console.WriteLine();
            Console.WriteLine("test3");
            res = service.Logout("user@gmail.com");
            Console.WriteLine();
            Console.WriteLine("test4");
            res = service.Register("user2@gmail.com", "Passw0rd");
           
            Console.WriteLine();
            Console.WriteLine("test5");

            res = service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "tit", "desc1", DateTime.Now.AddDays(1));
            Console.WriteLine();
            Console.WriteLine("test6");
            res = service.JoinBoard("user2@gmail.com", "user@gmail.com", "board11");
            Console.WriteLine();
            Console.WriteLine("test7");
            
            Response<Task> task1 = service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "tit", "desc1", DateTime.Now.AddDays(1));
            Console.WriteLine();
            Console.WriteLine("test8");
            Response<Task> task2 = service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "tit2", "desc2", DateTime.Now.AddDays(1));

            res = service.AdvanceTask("user2@gmail.com", "user@gmail.com", "board11", 0, task1.Value.Id);

            res = service.AdvanceTask("user2@gmail.com", "user@gmail.com", "board11", 0, task2.Value.Id);
            Console.WriteLine();
            Console.WriteLine("test9");
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");

           Console.WriteLine(ok.Value.Count);

            res = service.Logout("user2@gmail.com");
            res = service.Register("user3@gmail.com", "PASwo0rdd");

            Console.WriteLine();
            Console.WriteLine("test10");
            ok = service.InProgressTasks("user3@gmail.com");
            Console.WriteLine(ok.Value.Count);
            Console.WriteLine("test10");
            Console.WriteLine();
            service.Logout("user3@gmail.com");
            res=service.Login("user@gmail.com", "Passw0rd");
            Console.WriteLine("test 11");
              res = service.RemoveBoard("user@gmail.com", "user@gmail.com", "board11");
            Console.WriteLine("user@gmail.com " + "  has " + service.InProgressTasks("user@gmail.com").Value.Count);
            service.Logout("user@gmail.com");
            service.Login("user2@gmail.com", "Passw0rd");
            ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("test12");
            Console.WriteLine(ok.Value.Count);
            service.Logout("user2@gmail.com");
            service.Login("user@gmail.com", "Passw0rd");
            
            Console.WriteLine("test 13: recreating board 11 after deleting");
            Console.WriteLine();
            Console.WriteLine();
            service.AddBoard("user@gmail.com", "board11");

            Console.WriteLine("test 14 : let another user limit board 11");
            Console.WriteLine();
            Console.WriteLine();
            service.Logout("user@gmail.com");
            service.Login("user3@gmail.com", "PASwo0rdd");
            service.LimitColumn("user3@gmail.com", "user@gmail.com", "board11", 0, 3);
            service.Logout("user3@gmail.com");

            service.Login("user@gmail.com", "Passw0rd");

            Console.WriteLine("test 15 :  entering limit 3 and inserting 4 tasks");
            Console.WriteLine();
            Console.WriteLine();
            service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 3);
            service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit1", "desc1", DateTime.Now.AddDays(1));
            service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit2", "desc1", DateTime.Now.AddDays(1));
           task2= service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit3", "desc1", DateTime.Now.AddDays(1));
            service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit4", "desc1", DateTime.Now.AddDays(1));

            Console.WriteLine("test16 :  entering limit 2 with 3 tasks in");
            Console.WriteLine();
            Console.WriteLine();
            service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 2);

            Console.WriteLine("test17 :  moving  task then setting limit");
            Console.WriteLine();
            Console.WriteLine();
            service.AdvanceTask("user@gmail.com", "user@gmail.com", "board11", 0, task2.Value.Id);
           
            service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 2);
            Console.WriteLine("test18 :  try to create another board with name board11 using same user");
            Console.WriteLine();
            Console.WriteLine();
            service.AddBoard("user@gmail.com", "board11");

            Console.WriteLine("test19 :  try to create another board with name board11 using other user");
            Console.WriteLine();
            Console.WriteLine();
            service.Logout("user@gmail.com");
            service.Login("user3@gmail.com", "PASwo0rdd");
            service.AddBoard("user3@gmail.com", "board11");

            Console.WriteLine("test20 :  try to join another board with name board11 while having the name board11 ");
            Console.WriteLine();
            Console.WriteLine();
            service.JoinBoard("user3@gmail.com", "user@gmail.com", "board11");

            Console.WriteLine("test21 : try  more than one account to join same board");
            Console.WriteLine();
            Console.WriteLine();

            service.Logout("user3@gmail.com");
            service.Login("user2@gmail.com", "Passw0rd");
            service.JoinBoard("user2@gmail.com", "user@gmail.com", "board11");
            service.JoinBoard("user2@gmail.com", "user3@gmail.com", "board11");
            service.JoinBoard("user2@gmail.com", "user@gmail.com", "board1221");
            service.JoinBoard("user3@gmail.com", "user@gmail.com", "board11");

            Console.WriteLine("test22 : try  adding tasks to board 11 by 3 accounts");
            Console.WriteLine();
            Console.WriteLine();
            service.LimitColumn("user2@gmail.com", "user@gmail.com", "board11", 0, 10);
            service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "one", "one", DateTime.Now.AddDays(1));
            service.Logout("user2@gmail.com");
            service.Login("user@gmail.com", "Passw0rd");
            service.AddTask("user@gmail.com", "user@gmail.com", "board11", "two", "two", DateTime.Now.AddDays(1));
            service.AddTask("user3@gmail.com", "user@gmail.com", "board11", "three", "three", DateTime.Now.AddDays(1));

            service.Login("user3@gmail.com", "PASwo0rdd");
            service.Logout("user@gmail.com");

            service.Login("user3@gmail.com", "PASwo0rdd");
            service.AddTask("user3@gmail.com", "user@gmail.com", "board11", "three", "three", DateTime.Now.AddDays(1));
            */



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
             Response res = service.LoadData();
             res = service.Login("user@gmail.com", "Passw0rd");

             res = service.AddBoard("user@gmail.com", "fucker1");
             res = service.Logout("user@gmail.com");
             res = service.Register("user2@gmail.com", "Passw0rd");
             res = service.JoinBoard("user2@gmail.com", "user@gmail.com", "fucker1");
             Response<Task> task1 = service.AddTask("user2@gmail.com", "user@gmail.com", "fucker1", "tit", "desc1", DateTime.Now.AddDays(1));
             Response<Task> task2 = service.AddTask("user2@gmail.com", "user@gmail.com", "fucker1", "tit2", "desc2", DateTime.Now.AddDays(1));
             res = service.AdvanceTask("user2@gmail.com", "user@gmail.com", "fucker1", 0, task1.Value.Id);
             res = service.AdvanceTask("user2@gmail.com", "user@gmail.com", "fucker1", 0, task2.Value.Id);
             res = service.Logout("user2@gmail.com");
             res = service.Register("user3@gmail.com", "Passw0rd");
             service.Logout("user3@gmail.com");
             service.Login("user@gmail.com", "Passw0rd");
             service.AddBoard("user@gmail.com", "board11");
             service.Logout("user@gmail.com");
             service.Login("user3@gmail.com", "PASwo0rdd");
             service.LimitColumn("user3@gmail.com", "user@gmail.com", "board11", 0, 3);
             service.Logout("user3@gmail.com");
             
             service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 3);
             service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit1", "desc1", DateTime.Now.AddDays(1));
             service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit2", "desc1", DateTime.Now.AddDays(1));
             task2 = service.AddTask("user@gmail.com", "user@gmail.com", "board11", "tit3", "desc1", DateTime.Now.AddDays(1));
             service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 2);
             service.AdvanceTask("user@gmail.com", "user@gmail.com", "board11", 0, task2.Value.Id);
             service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, 2);
             service.JoinBoard("user3@gmail.com", "user@gmail.com", "board11");
            */
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///


            service.LoadData();
            service.Login("user@gmail.com", "Passw0rd");
            //service.LimitColumn("user@gmail.com", "user@gmail.com", "board11", 0, -1);
            service.RemoveBoard("user@gmail.com", "user@gmail.com", "fucker1");
            
          
            service.LoadData();
            service.Login("user@gmail.com", "Passw0rd");
            service.RemoveBoard("user@gmail.com", "user@gmail.com", "fucker1");

























































            /*
            
            Console.WriteLine("test23 : try  adding tasks to board 11 after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();
            Response res = service.LoadData();
            service.Login("user2@gmail.com", "Passw0rd");
            service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "four", "four", DateTime.Now.AddDays(1));



            Console.WriteLine("test24 : try  deleting board11 after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();
            service.RemoveBoard("user2@gmail.com", "user@gmail.com", "board11");
            service.Logout("user2@gmail.com");
            service.Login("user@gmail.com", "Passw0rd");
            service.RemoveBoard("user@gmail.com", "user@gmail.com", "board11");



            Console.WriteLine("test25 : try  adding tasks to a deleted board11 after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();

            service.Logout("user@gmail.com");
            service.Login("user2@gmail.com", "Passw0rd");
            service.AddTask("user2@gmail.com", "user@gmail.com", "board11", "five", "five", DateTime.Now.AddDays(1));

            Console.WriteLine("test26 : try  adding tasks to a new board after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();


            service.AddBoard("user2@gmail.com", "bayanboard");
            Response<Task> task3=service.AddTask("user2@gmail.com", "user2@gmail.com", "bayanboard", "one", "one", DateTime.Now.AddDays(1));
            service.AdvanceTask("user2@gmail.com", "user2@gmail.com", "bayanboard", 0, task3.Value.Id);
            service.AddTask("user2@gmail.com", "user2@gmail.com", "bayanboard", "two", "two", DateTime.Now.AddDays(1));
            service.AddTask("user2@gmail.com", "user2@gmail.com", "bayanboard", "3", "3", DateTime.Now.AddDays(1));
            Response<Task> task4=service.AddTask("user2@gmail.com", "user2@gmail.com", "bayanboard", "four", "four", DateTime.Now.AddDays(1));
            service.AdvanceTask("user2@gmail.com", "user2@gmail.com", "bayanboard", 0, task4.Value.Id);

            Console.WriteLine("test27 : try  getting inprogress tasks  existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("numbeer of tasks is : " + ok.Value.Count);
            */








            /*

            
            Console.WriteLine("test28 : try  adding  tasks to board 11 after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();

            service.LoadData();
            service.Login("user2@gmail.com", "Passw0rd");

            

            
            Response<Task> task3 = service.AddTask("user2@gmail.com", "user3@gmail.com", "board11", "five", "five", DateTime.Now.AddDays(1));


            Console.WriteLine("test29 : try  advacning tasksand get inproggess from 2 boards after existing program and loading data");
            Console.WriteLine();
            Console.WriteLine();
            service.AdvanceTask("user2@gmail.com", "user@gmail.com", "board11", 0, task3.Value.Id);
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("numbeer of tasks is : " + ok.Value.Count);
            */

            /*
            service.LoadData();
            service.Login("user2@gmail.com", "Passw0rd");



            Response<Task> task3 = service.AddTask("user2@gmail.com", "user3@gmail.com", "board11", "five", "five", DateTime.Now.AddDays(1));

            service.AdvanceTask("user2@gmail.com", "user2@gmail.com", "board11", 0, task3.Value.Id);
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("numbeer of tasks is : " + ok.Value.Count);
            */

            /*
            service.LoadData();
            service.Login("user2@gmail.com", "Passw0rd");

            Response<Task> task3 = service.AddTask("user2@gmail.com", "user3@gmail.com", "board11", "six", "six", DateTime.Now.AddDays(1));

            service.AdvanceTask("user2@gmail.com", "user3@gmail.com", "board11", 0, task3.Value.Id);
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("numbeer of tasks is : " + ok.Value.Count);
            */

            /*
            service.LoadData();
            service.Login("user2@gmail.com", "Passw0rd");

            
            service.AdvanceTask("user2@gmail.com", "user2@gmail.com", "bayanboard", 1, 50088783);
            service.AdvanceTask("user2@gmail.com", "user3@gmail.com", "board11", 1, 64486839);
            Response<IList<Task>> ok = service.InProgressTasks("user2@gmail.com");
            Console.WriteLine("numbeer of tasks is : " + ok.Value.Count);
            */

         


            /*
            res = service.Register("user@gmail.com", "Passw0rd");
            res = service.Logout("user@gmail.com");
            res = service.Register("user@@gmail.com", "Passw0rd");
            res = service.Logout("user@@gmail.com");
            res = service.Register("user@gmail..com", "Passw0rd");
            res = service.Logout("user@gmail..com");
            res = service.Register("usergmail.com", "Passw0rd");
            res = service.Logout("user@gmail.com");

            res = service.Register("user2@gmail.com", "Passw0rd");
            res = service.Logout("user2@gmail.com");

            res = service.Login("user@gmail.com", "Passw0rd");
            
            
            res = service.Logout("user@gmail.com");

            Response<User> resuser = service.Login("user@gmail.com", "Passw0rd");

            res = service.AddBoard(resuser.Value.Email, "Board11");// todo Response<Board> res=....

            Response<Task> res2 = service.AddTask(resuser.Value.Email, "user@gmail.com", "Board11", "tit", "desc1", DateTime.Now.AddDays(1));
            Response<Task> res3 = service.AddTask(resuser.Value.Email, "user@gmail.com", "Board11", "tit3", "desc3", DateTime.Now.AddDays(1));
            res = service.AdvanceTask(resuser.Value.Email, "user@gmail.com", "Board11", 0, (res3).Value.Id);

            res = service.GetColumn(resuser.Value.Email, "user@gmail.com", "Board11", 0);



            res = service.Logout("user@gmail.com");


            Response<User> resuser2 = service.Login("user2@gmail.com", "Passw0rd");
                        res = service.AddBoard(resuser2.Value.Email, "Board11");// todo Response<Board> res=....

            res = service.JoinBoard("user2@gmail.com", "user@gmail.com", "Board11");

            res = service.GetColumn(resuser2.Value.Email, "user@gmail.com", "Board11", 0);

            res = service.AssignTask(resuser2.Value.Email, "user@gmail.com", "Board11", 0,res2.Value.Id, resuser2.Value.Email);
            res = service.AdvanceTask(resuser2.Value.Email, "user@gmail.com", "Board11", 0, (res2).Value.Id);
            res = service.InProgressTasks(resuser2.Value.Email);
            res = service.AdvanceTask(resuser2.Value.Email, "user@gmail.com", "Board11",1, (res2).Value.Id);
            res = service.InProgressTasks(resuser2.Value.Email);

            res = service.RemoveBoard(resuser2.Value.Email, resuser.Value.Email, "Board11");

            res = service.Logout("user2@gmail.com");


            resuser = service.Login("user@gmail.com", "Passw0rd");

            res = service.InProgressTasks(resuser.Value.Email);
            res = service.RemoveBoard(resuser.Value.Email, resuser.Value.Email, "Board11");

            res = service.DeleteData();
            */


            //**************************************************************

            /*

            res = service.AddBoard(resuser.Value.Email, "board11");
            

            res = service.AddBoard(resuser.Value.Email, "board11");

            res = service.RemoveBoard(resuser.Value.Email, "user@gmail.com", "board11");


            res = service.RemoveBoard(resuser.Value.Email, "user@gmail.com", "board11");

            res = service.AddBoard(resuser.Value.Email, "Board11");


            res = service.GetColumnName(resuser.Value.Email, "user@gmail.com", "Board11", 0);

            res = service.GetColumnName(resuser.Value.Email, "user@gmail.com", "Board11", 1);

            res = service.GetColumnName(resuser.Value.Email, "user@gmail.com", "Board11", 2);


            res = service.LimitColumn(resuser.Value.Email, "user@gmail.com", "Board11", 0, 2);


            res = service.GetColumnLimit(resuser.Value.Email, "user@gmail.com", "Board11", 0);


            Response<Task> res2 = service.AddTask(resuser.Value.Email, "user@gmail.com", "Board11", "tit", "desc1", DateTime.Now.AddDays(1));

            
            /*

            res = service.AdvanceTask(resuser.Value.Email, "user@gmail.com", "Board11", 0, (res2).Value.Id);

            res = service.AdvanceTask("user@gmail.com", "user@gmail.com", "Board11", 0, (res2).Value.Id);

            res = service.UpdateTaskTitle("user@gmail.com", "user@gmail.com", "Board11", 1, (res2).Value.Id, "update1");

            res = service.InProgressTasks("user@gmail.com");

            if (res.ErrorOccured)
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("*************************");
                Response<IList<Task>> rescast = (Response<IList<Task>>)res;
                foreach (Task t in rescast.Value)
                {
                    Console.WriteLine(t.Title);
                }
                Console.WriteLine(rescast.Value.ToString());
            }
            res = service.AdvanceTask("user@gmail.com", "user@gmail.com", "Board11", 3, (res2).Value.Id);

            if (res.ErrorOccured)
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("test completed!");
            }
            Response<Task> res3 = service.AddTask("user@gmail.com", "user@gmail.com", "Board11", "title2", "desc2", DateTime.Now.AddDays(1));

            if (res.ErrorOccured)
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("test completed!");
            }
            Response<Task> res4 = service.AddTask("user@gmail.com", "user@gmail.com","Board11", "title", "Title12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890Title12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", DateTime.Now.AddDays(1));

            if (res.ErrorOccured)
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("test completed!");
            }
            res = service.GetColumn("user@gmail.com", "user@gmail.com", "Board11", 0);

            if (res.ErrorOccured)
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("test completed!");
            }


            */









        }
    }
}

