using PresentaionLayer;

namespace Presentation.Model
{
    public abstract class NotifiableModelObject : NotifiableObject
    {
        private string creator_mail;
        public BackendController Controller { get; private set; }
        protected NotifiableModelObject(BackendController controller)
        {
            this.Controller = controller;
        }
    }
}
