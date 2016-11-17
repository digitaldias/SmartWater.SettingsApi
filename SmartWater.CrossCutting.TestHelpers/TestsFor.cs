using Moq;
using StructureMap.AutoMocking.Moq;

namespace SmartWater.CrossCutting.TestHelpers
{
    public abstract class TestsFor<TInstance> where TInstance : class
    {
        protected TInstance Instance { get; set; }

        protected MoqAutoMocker<TInstance> AutoMock { get; set; }


        public TestsFor()
        {
            AutoMock = new MoqAutoMocker<TInstance>();

            OverrideMocks();

            Instance = AutoMock.ClassUnderTest;
        }

        public virtual void OverrideMocks(){
        }

        /// <summary>
        /// Use within an override OverrideMocks() only. Otherwise
        /// this will make no sense. Allows you to replace an mocked instance 
        /// of TContract with your own. 
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="with"></param>
        public void Inject<TContract>(TContract with) where TContract : class
        {
            AutoMock.Container.Inject<TContract>(with);
        }


        public Mock<TContract> GetMockFor<TContract>() where TContract : class
        {
            return Mock.Get(AutoMock.Get<TContract>());
        }
    }
}
