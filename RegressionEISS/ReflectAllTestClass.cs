using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RegressionEISS
{
    public class ReflectAllTestClass
    {
        Type baseTestType = typeof(NetSeleniumBaseUnitTest);

        public List<string> ExecuteTests(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> errorList = new List<string>();

            var subclasses =
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetTypes()
            where type.IsSubclassOf(typeof(NetSeleniumBaseUnitTest))
            select type;

            // execute all test methods in the test classes
            foreach (Type type in subclasses)
            {
                var methods =
                    from typeMth in type.GetMethods()
                    where typeMth.Name.EndsWith(TestConstants.MethodsToTestEndWith)
                    select typeMth;
                foreach (MethodInfo method in methods)
                {
                    String error = InvokeTestMethod(type, method.Name);
                    if (String.IsNullOrEmpty(error))
                    {
                        errorList.Add(error);
                    }
                }

                //InvokeDriverMethod(type, TestConstants.MethodsCloseDriver);
            }

            return errorList;
        }

        public String InvokeTestMethod(Type testType, String methodName)
        {
            String error = String.Empty;
            ConstructorInfo testConstructor = testType.GetConstructor(Type.EmptyTypes);
            object testClassObject = testConstructor.Invoke(new object[] { });

            InvokeDriverMethod(testClassObject, TestConstants.MethodsInitialDriver);
            // Get the ItsMagic method and invoke with a empty parameter 
            try
            {
                MethodInfo testMethod = testType.GetMethod(methodName);
                testMethod.Invoke(testClassObject, new object[] { });
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            InvokeDriverMethod(testClassObject, TestConstants.MethodsCloseDriver);
            return error;
        }

        //public void InvokeDriverMethod(Type classType, String methodName)
        //{
        //    ConstructorInfo testConstructor = classType.GetConstructor(Type.EmptyTypes);
        //    object testClassObject = testConstructor.Invoke(new object[] { });

        //    MethodInfo initialMethod = classType.GetMethod(methodName);
        //    initialMethod.Invoke(testClassObject, new object[] { });
        //}

        public void InvokeDriverMethod(object driverObject, String methodName)
        {
            MethodInfo initialMethod = baseTestType.GetMethod(methodName);
            initialMethod.Invoke(driverObject, new object[] { });
        }
    }
}
