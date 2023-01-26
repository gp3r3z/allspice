import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data

      this.getFavoriteData()
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }


  async getMyFavorite() {
    // FIXME working on updating the appstate after unfavoriting for reactivity
    // logger.log('FE response', responseData)
    const favoriteData = AppState.unalterFav

    if (AppState.favorites != '') {
      AppState.recipes = AppState.favorites
    }
    else {
      favoriteData.map(data => {
        const same = AppState.recipes.filter(obj => {
          return data.recipeId == obj.id
        })
        // logger.log("Here is the same ", same)
        AppState.favorites.push(same[0])
        return same
      })
      AppState.recipes = AppState.favorites

    }
    logger.log(AppState.favorites, ' favorites updated')

  }
  async getFavoriteData() {
    const res = await api.get(`account/favorites`)

    // NOTE created another appstate to use the data to compare against 
    AppState.unalterFav = res.data
    logger.log('Getting Fav data ', AppState.unalterFav)
  }
}

export const accountService = new AccountService()
