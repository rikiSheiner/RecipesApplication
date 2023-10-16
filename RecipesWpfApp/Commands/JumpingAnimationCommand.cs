using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace RecipesWpfApp.Commands
{
    internal class JumpingAnimationCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                DoubleAnimation jumpAnimation = new DoubleAnimation
                {
                    To = -10,  
                    Duration = TimeSpan.FromSeconds(0.5),
                    AutoReverse = true
                };

                TranslateTransform transform = new TranslateTransform();
                element.RenderTransform = transform;
                transform.BeginAnimation(TranslateTransform.YProperty, jumpAnimation);
            }
        }
    }
}
